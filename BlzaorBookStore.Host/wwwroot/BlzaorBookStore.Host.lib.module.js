// Blazor JS initializer — branded loading screen with progress bar.
// Pattern from MS docs: "Global Interactive WebAssembly rendering without prerendering".
// Blazor automatically updates --blazor-load-percentage on the document element during boot,
// but that value hits 100% as soon as bytes are downloaded — well before the runtime is
// actually ready. We mirror that value into our own --app-load-percentage capped at 90%,
// then snap to 100% only when afterWebAssemblyStarted fires, so the bar never sits at 100%
// while the user is still waiting.

const LOADER_ID = 'app-loading-screen';
const APP_VAR = '--app-load-percentage';
const BLAZOR_VAR = '--blazor-load-percentage';
const PROGRESS_CAP_PCT = 90;
let pollHandle = 0;

function injectStyles() {
    if (document.getElementById('app-loading-styles')) return;
    const style = document.createElement('style');
    style.id = 'app-loading-styles';
    style.textContent = `
        :root { ${APP_VAR}: 0%; }
        #${LOADER_ID} {
            position: fixed;
            inset: 0;
            background: linear-gradient(135deg, #1e293b 0%, #0f172a 100%);
            color: #f1f5f9;
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            z-index: 9999;
            font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, sans-serif;
            transition: opacity 0.4s ease-out;
        }
        #${LOADER_ID}.fade-out { opacity: 0; pointer-events: none; }

        #${LOADER_ID} .brand {
            font-size: 1.75rem;
            font-weight: 600;
            letter-spacing: 0.5px;
            margin-bottom: 0.5rem;
        }
        #${LOADER_ID} .tagline {
            font-size: 0.95rem;
            color: #94a3b8;
            margin-bottom: 2.5rem;
        }
        #${LOADER_ID} .progress-track {
            width: min(360px, 80vw);
            height: 6px;
            background: rgba(255,255,255,0.1);
            border-radius: 999px;
            overflow: hidden;
            position: relative;
        }
        #${LOADER_ID} .progress-fill {
            position: absolute;
            inset: 0;
            background: linear-gradient(90deg, #3b82f6, #8b5cf6);
            border-radius: 999px;
            transform-origin: left center;
            transform: scaleX(var(${APP_VAR}, 0%));
            transition: transform 0.6s cubic-bezier(0.4, 0, 0.2, 1);
        }
        #${LOADER_ID} .progress-text {
            margin-top: 1rem;
            font-size: 0.85rem;
            color: #cbd5e1;
            font-variant-numeric: tabular-nums;
        }
        #${LOADER_ID} .progress-text::after {
            content: var(${APP_VAR}, "Loading...");
        }
    `;
    document.head.appendChild(style);
}

function injectLoader() {
    if (document.getElementById(LOADER_ID)) return;
    const loader = document.createElement('div');
    loader.id = LOADER_ID;
    loader.setAttribute('aria-label', 'Loading application');
    loader.setAttribute('role', 'status');
    loader.innerHTML = `
        <div class="brand">Blazor Book Store</div>
        <div class="tagline">Preparing your library&hellip;</div>
        <div class="progress-track"><div class="progress-fill"></div></div>
        <div class="progress-text"></div>
    `;
    document.body.appendChild(loader);
}

function readBlazorPercent() {
    const raw = getComputedStyle(document.documentElement).getPropertyValue(BLAZOR_VAR).trim();
    const num = parseFloat(raw);
    return Number.isFinite(num) ? num : 0;
}

function setAppPercent(pct) {
    document.documentElement.style.setProperty(APP_VAR, `${pct}%`);
}

function startPolling() {
    pollHandle = setInterval(() => {
        const blazorPct = readBlazorPercent();
        setAppPercent(Math.min(blazorPct, PROGRESS_CAP_PCT));
    }, 100);
}

function stopPolling() {
    if (pollHandle) {
        clearInterval(pollHandle);
        pollHandle = 0;
    }
}

export function beforeWebStart(options) {
    injectStyles();
    injectLoader();
    setAppPercent(0);
    startPolling();
}

export function afterWebAssemblyStarted(blazor) {
    stopPolling();
    setAppPercent(100);
    const loader = document.getElementById(LOADER_ID);
    if (!loader) return;
    // Wait for the bar to animate to 100% (matches CSS transition), then fade out.
    setTimeout(() => {
        loader.classList.add('fade-out');
        setTimeout(() => loader.remove(), 400);
    }, 700);
}
