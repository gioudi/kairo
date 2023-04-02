/*
 * Toasts - Advanced UI 
 */

// Basic Toast

/*
 * message - El mensaje que se va a mostrar en pantalla
 * cls - la clase de toast (normal, rounded, etc)
 */
function toast(message, cls) {
    if (cls === undefined) {
        cls = ''
    }
    M.Toast.dismissAll();
    M.toast({
        html: message,
        classes: cls
    })
}
