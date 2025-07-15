// Universal Enter key submit for all forms
//function enableEnterSubmit() {
//    document.querySelectorAll("form").forEach(function (form) {
//        form.addEventListener("keypress", function (e) {
//            const tag = e.target.tagName.toUpperCase();

//            // Ignore Enter on textarea
//            if (tag === "TEXTAREA") return;

//            if ((tag === "INPUT" || tag === "SELECT") && e.key === "Enter") {
//                e.preventDefault();

//                // Ensure jQuery and validation are available
//                if (typeof $ !== "undefined" && typeof $.validator !== "undefined") {
//                    const $form = $(form);

//                    // Re-parse unobtrusive validation if needed (optional safety)
//                    if (!$form.data("validator")) {
//                        $.validator.unobtrusive.parse($form);
//                    }

//                    // Trigger form submission (respects validation)
//                    $form.trigger("submit");
//                } else {
//                    // Fallback: native submit
//                    form.requestSubmit();
//                }
//            }
//        });
//    });
//}


function focusFirstInput() {
    const firstInput = document.querySelector("form input:not([type=hidden]):not([disabled]), form select, form textarea");
    if (firstInput) {
        firstInput.focus();
    }
}


// Reusable SweetAlert utilities

function showSuccess(message) {
    Swal.fire({
        icon: 'success',
        title: message,
        showConfirmButton: false,
        timer: 2000,
        timerProgressBar: true
    });
}

function showError(message) {
    Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: message,
        showConfirmButton: true
    });
}

function showInfo(message) {
    Swal.fire({
        icon: 'info',
        title: 'Info',
        text: message,
        showConfirmButton: true
    });
}


function showConfirmation(title, text, confirmText, callback) {
    Swal.fire({
        title: title,
        text: text,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: confirmText,
        cancelButtonText: 'Cancel'
    }).then((result) => {
        if (result.isConfirmed) {
            callback(); // Run the action
        }
    });
}
