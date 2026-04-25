// Blazor JS initializer — branded loading screen with progress bar.
// Pattern from MS docs: "Global Interactive WebAssembly rendering without prerendering".
// Blazor automatically updates --blazor-load-percentage on the document element during boot.

const LOADER_ID = 'app-loading-screen';

function injectStyles() {
    if (document.getElementById('app-loading-styles')) return;
    const style = document.createElement('style');
    style.id = 'app-loading-styles';
    style.textContent = `
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
            transform: scaleX(var(--blazor-load-percentage, 0%));
            transition: transform 0.25s ease-out;
        }
        #${LOADER_ID} .progress-text {
            margin-top: 1rem;
            font-size: 0.85rem;
            color: #cbd5e1;
            font-variant-numeric: tabular-nums;
        }
        #${LOADER_ID} .progress-text::after {
            content: var(--blazor-load-percentage-text, "Loading...");
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

export function beforeWebStart(options) {
    injectStyles();
    injectLoader();
}

export function afterWebAssemblyStarted(blazor) {
    const loader = document.getElementById(LOADER_ID);
    if (!loader) return;
    loader.classList.add('fade-out');
    setTimeout(() => loader.remove(), 500);
}
