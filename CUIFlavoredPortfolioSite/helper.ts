namespace Helper {
    export function scrollIntoView(element: HTMLElement): void {
        element.scrollIntoView();
    }

    export function getScreenMetrics() {

        const span = document.createElement('span');
        span.innerText = '_';
        span.style.position = 'fixed';
        span.style.visibility = 'hidden';
        document.body.appendChild(span);
        const charWidthPx = span.offsetWidth;
        span.remove();

        const screenWidthChar = Math.floor( document.body.clientWidth / charWidthPx);

        return { charWidthPx, screenWidthChar };
    }

    let timerId: number | null = null;

    window.addEventListener("mouseup", function (event) {
        if (timerId !== null) this.clearTimeout(timerId);
        timerId = this.setTimeout(() => { resetFocus() }, 400);
    });

    function resetFocus(): void {
        const selection = window.getSelection();
        if (selection !== null && selection.type === 'Range') return;

        const commandLineInput = document.getElementById('command-line-input');
        if (commandLineInput !== null) commandLineInput.focus();
    }

    document.addEventListener('keydown', e => {
        if (e.code === 'ArrowUp' || e.code === 'ArrowDown' || e.code === 'Tab' || (e.code === 'KeyL' && e.ctrlKey)) {
            if ((e.srcElement as HTMLElement).id === 'command-line-input') {
                e.preventDefault();
            }
        }
    })
}