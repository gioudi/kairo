import IMask, { InputMask, Masked, MaskedNumber } from "imask";
import { IMaskFormat } from "./MaskFormats";

export class MaskFormatsManager {

    private Masks: Array<IMaskFormat>;
    private FormatElements: Array<IMaskElement>;

    constructor() {
        this.FormatElements = new Array<IMaskElement>();
    }

    public RegisterMasks(masks: Array<IMaskFormat>) {
        this.CleanAll();
        this.Masks = masks;
    }

    public ApplyMasks() {

        this.CleanFormats();

        this.Masks?.forEach((element, index, object) => {
            this.ApplyFormat(element);
        });
    }

    public AddApplyMask(mask: IMaskFormat) {
        this.Masks.push(mask);
        this.ApplyFormat(mask);
    }

    public UnmaskFormats() {
        this.FormatElements?.forEach((element, index, object) => {
            element.MaskInput.el['input'].value = element.MaskInput.unmaskedValue;
            element.MaskInput.masked.reset();
        });
    }

    public SetUnmaskedFormValue(formData: FormData) {

        this.FormatElements?.forEach((element, index, object) => {
            formData.set(element.MaskInput.el['input'].name, element.MaskInput.unmaskedValue);
            element.MaskInput.masked.reset();
        });
    }

    public UpdateValue(selectors: string[]) {

        if (selectors?.length > 0) {
            selectors.forEach((val) => {
                let formatEl = this.FormatElements.filter(e => e.Selector === val);
                if (formatEl?.length > 0) {
                    formatEl.forEach((el) => {
                        el.MaskInput.updateValue();
                        el.MaskInput.unmaskedValue = el.MaskInput.el.value;
                    })
                }
            })
        }
    }

    private ApplyFormat(mask: IMaskFormat) {
        var fields = document.querySelectorAll<HTMLElement>(mask.Selector);
        fields.forEach((element) => {
            this.FormatElements.push({
                Selector: mask.Selector, MaskInput: IMask(element, mask.Mask)
            });
        });

    }

    private CleanAll() {

        if (this.Masks?.length > 0)
            this.Masks.forEach((element, index, object) => {
                object.splice(index, 1);
            });

        this.CleanFormats();

    }

    private CleanFormats() {
        if (this.FormatElements?.length > 0)
            this.FormatElements.forEach((element, index, object) => {
                element.MaskInput.masked.reset();
                element.MaskInput.destroy;
                object.splice(index, 1);
            });
    }

    public Destroy() {
        this.CleanAll();
        this.Masks = null;
        this.FormatElements = null;
    }

}

export interface IMaskElement {
    Selector: string;
    MaskInput: InputMask<IMask.AnyMaskedOptions>;
}
