"use strict";
var Helper;
(function (Helper) {
    function scrollIntoView(element) {
        element.scrollIntoView();
    }
    Helper.scrollIntoView = scrollIntoView;
    function getScreenMetrics() {
        var span = document.createElement('span');
        span.innerText = '_';
        span.style.position = 'fixed';
        span.style.visibility = 'hidden';
        document.body.appendChild(span);
        var charWidthPx = span.offsetWidth;
        span.remove();
        var screenWidthChar = Math.floor(document.body.clientWidth / charWidthPx);
        return { charWidthPx: charWidthPx, screenWidthChar: screenWidthChar };
    }
    Helper.getScreenMetrics = getScreenMetrics;
    var timerId = null;
    window.addEventListener("mouseup", function (event) {
        if (timerId !== null)
            this.clearTimeout(timerId);
        timerId = this.setTimeout(function () { resetFocus(); }, 400);
    });
    function resetFocus() {
        var selection = window.getSelection();
        if (selection !== null && selection.type === 'Range')
            return;
        var commandLineInput = document.getElementById('command-line-input');
        if (commandLineInput !== null)
            commandLineInput.focus();
    }
    document.addEventListener('keydown', function (e) {
        if (e.code === 'ArrowUp' || e.code === 'ArrowDown' || e.code === 'Tab' || (e.code === 'KeyL' && e.ctrlKey)) {
            if (e.srcElement.id === 'command-line-input') {
                e.preventDefault();
            }
        }
    });
})(Helper || (Helper = {}));
