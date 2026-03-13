var $debugHelper = $debugHelper || {};
$debugHelper = function () {

    const debugStyleId = 'debugger-inline-style';

    function addCss() {
        if (!document.head) return;
        if (document.getElementById(debugStyleId)) return;

        const style = document.createElement('style');
        style.id = debugStyleId;
        style.textContent = `
            * {
                outline: 1px solid red !important;
            }
            *:hover {
                outline: 2px solid blue !important;
            }
        `;
        document.head.appendChild(style);
    }

    function removeCss() {
        const style = document.getElementById(debugStyleId);
        if (style) {
            style.remove();
        }
    }

    /*
     * Exposed functions
     */
    return {
        addCss: addCss,
        removeCss: removeCss
    };
}();