declare global {
    interface HTMLElement {
        on(eventType: string, childSelector: string, eventHandler: any): void;
    }
}

HTMLElement.prototype.on = function (eventType: string, childSelector: string, eventHandler: any): void {
    this.addEventListener(eventType, eventOnElement => {
        if (eventOnElement.target.matches(childSelector)) {
            eventHandler(eventOnElement);
        } else {
            //console.log(eventOnElement.target);
        }
    })
}

export { };