export class ConfirmModalMessage {

    private parent: HTMLElement;
    private modal: HTMLElement;
    private acceptButton: HTMLButtonElement;
    private dismissButton: HTMLButtonElement;
    private modalInstance: M.Modal;
    private idMesssage: string;

    constructor(public title: string,
        public message: string,
        public acceptText: string,
        public dismissText: string) {

        this.idMesssage = "confirm-message";
        this.parent = document.body;
        this.CreateModal();
        this.AppendModel();
    }

    private CreateModal() {
        this.modal = document.createElement("div");
        this.modal.id = this.idMesssage;
        this.modal.classList.add("modal", "modal-fixed-footer", "modal-confirm-message-small");

        const content = document.createElement("div");
        content.classList.add("modal-content");
        this.modal.appendChild(content);

        const header = document.createElement("h5");
        header.textContent = this.title;
        content.appendChild(header);

        const message = document.createElement("p");
        message.textContent = this.message;
        content.appendChild(message);

        const buttonGroup = document.createElement("div");
        buttonGroup.classList.add("modal-footer");
        this.modal.appendChild(buttonGroup);

        this.dismissButton = document.createElement("button");
        this.dismissButton.classList.add(
            "modal-action",
            "waves-effect",
            "waves-red",
            "btn-flat",
            "btn-orange"
        );
        this.dismissButton.type = "button";
        this.dismissButton.textContent = this.dismissText;
        buttonGroup.appendChild(this.dismissButton);

        this.acceptButton = document.createElement("button");
        this.acceptButton.classList.add(
            "modal-action",
            "waves-effect",
            "waves-red",
            "btn-flat",
            "btn-orange"
        );
        this.acceptButton.type = "button";
        this.acceptButton.textContent = this.acceptText;
        buttonGroup.appendChild(this.acceptButton);
    }

    private AppendModel() {
        this.parent.appendChild(this.modal);
    }

    private destroy() {
        if (this.modalInstance) {
            this.modalInstance.close();
            this.modalInstance.destroy();
        }

        var elem = document.querySelector("#" + this.idMesssage);
        elem.parentNode.removeChild(elem);
    }

    public async Confirm() {
        return new Promise<boolean>((resolve, reject) => {

            const somethingWentWrongUponCreation =
                !this.modal || !this.acceptButton || !this.dismissButton;
            if (somethingWentWrongUponCreation) {
                this.destroy();
                reject("Fallo algo en la creación del mensaje");
            }

            M.Modal.init(this.modal, {
                dismissible: false,
                opacity: .5,
                inDuration: 300,
                outDuration: 200,
                startingTop: '6%',
                endingTop: '8%'
            });
            this.modalInstance = M.Modal.getInstance(this.modal);
            this.modalInstance.open();

            this.acceptButton.addEventListener("click", () => {
                resolve(true);
                this.destroy();
            });

            this.dismissButton.addEventListener("click", () => {
                resolve(false);
                this.destroy();
            });
        });
    }
}