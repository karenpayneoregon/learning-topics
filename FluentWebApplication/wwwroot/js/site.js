
document.addEventListener('DOMContentLoaded',
    () => {
        document.addEventListener('keydown',
            function (event) {
                if (event.key === '1' && event.altKey && event.ctrlKey) {
                    if (!document.getElementById('debugger-inline-style')) {
                        $debugHelper.addCss();
                    } else {
                        $debugHelper.removeCss();
                    }
                }
            });
    });