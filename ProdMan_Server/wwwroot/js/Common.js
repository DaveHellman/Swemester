window.ShowToast = (type, message) => {
    if (type === "success") {
        toastr.success(message, "Success!");
    } else if (type === "error") {
        toastr.error(message, "Operation failed!");
    }
}