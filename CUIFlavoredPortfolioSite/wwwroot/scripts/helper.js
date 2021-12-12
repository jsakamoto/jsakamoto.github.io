"use strict";
var Helper;
(function (Helper) {
    function scrollIntoView(element) {
        element.scrollIntoView();
    }
    Helper.scrollIntoView = scrollIntoView;
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
