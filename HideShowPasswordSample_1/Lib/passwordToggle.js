document.addEventListener("DOMContentLoaded", () => {

    // debug helper key handler (kept from original)
    document.addEventListener('keydown', function (event) {
        if (event.key === '1' && event.altKey && event.ctrlKey) {
            if (typeof $debugHelper !== 'undefined' && $debugHelper.toggle) {
                $debugHelper.toggle(true);
            }
        }
    });

    const icons = document.querySelectorAll('.password-wrapper .icon');

    icons.forEach(icon => {
        const passwordInput = icon.previousElementSibling; // the <input> just before the <i>

        if (!passwordInput) return;

        const showPassword = () => {
            if (!passwordInput.classList.contains('password')) return;

            passwordInput.type = 'text';

            // optional: change icon while showing
            icon.classList.remove('fa-eye');
            icon.classList.add('fa-eye-slash');
        };

        const hidePassword = () => {
            if (!passwordInput.classList.contains('password')) return;

            passwordInput.type = 'password';

            // optional: change icon back when hiding
            icon.classList.add('fa-eye');
            icon.classList.remove('fa-eye-slash');
        };

        //
        // Desktop: PRESS & HOLD behavior
        //
        icon.addEventListener('mousedown', (e) => {
            showPassword();
        });

        icon.addEventListener('mouseup', (e) => {
            hidePassword();
        });

        icon.addEventListener('mouseleave', (e) => {
            hidePassword();
        });

        // Mobile: PRESS & HOLD behavior using touch
        // https://developer.mozilla.org/en-US/docs/Web/API/Element/touchstart_event
        icon.addEventListener('touchstart', (e) => {
            e.preventDefault();
            showPassword();
        }, { passive: false });

        icon.addEventListener('touchend', (e) => {
            hidePassword();
        });

        icon.addEventListener('touchcancel', (e) => {
            hidePassword();
        });
    });

});
