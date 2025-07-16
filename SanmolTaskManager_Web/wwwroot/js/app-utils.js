// Universal focus First Input for all forms
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
