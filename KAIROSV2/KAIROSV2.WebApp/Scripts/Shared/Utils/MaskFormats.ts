import { format, parse, parseISO, toDate } from "date-fns";
import IMask, { InputMask, Masked, MaskedDate, MaskedDynamic, MaskedNumber } from "imask";

export class MaskFormats {

    public static IntegerFormat(): IMaskFormat {
        return {
            Selector: ".formato-int",
            Mask: {
                mask: Number,
                scale: 0,
                signed: false,
                thousandsSeparator: ',',
                padFractionalZeros: false,
                normalizeZeros: true,
                radix: '.',
                mapToRadix: ['.']
            }
        };
    }

    public static DecimalFormat(): IMaskFormat {
        return {
            Selector: ".formato-decimal",
            Mask: {
                mask: Number,
                scale: 2,
                signed: false,
                thousandsSeparator: ',',
                padFractionalZeros: false,
                normalizeZeros: true,
                radix: '.',
                mapToRadix: ['.']
            }
        };
    }

    public static DateShortFormat(): IMaskFormat {
        return {
            Selector: ".formato-fecha-corta",
            Mask: {
                mask: '00{/}MMM{/}0000',
                overwrite: true,
                autofix: true,
                blocks:
                {
                    MMM: {
                        mask: IMask.MaskedEnum,
                        enum: ['ene', 'feb', 'mar', 'abr', 'may', 'jun', 'jul', 'ago', 'sep', 'oct', 'nov', 'dic']
                    }
                }
            }
        };
    }

    public static DateLongFormat(): IMaskFormat {
        return {
            Selector: ".formato-fecha-larga",
            Mask: {
                mask: '00{/}MMM{/}0000{ }HH{:}TT{:}TT',
                overwrite: true,
                autofix: true,
                blocks:
                {
                    MMM: {
                        mask: IMask.MaskedEnum,
                        enum: ['ene', 'feb', 'mar', 'abr', 'may', 'jun', 'jul', 'ago', 'sep', 'oct', 'nov', 'dic']
                    },
                    HH: {
                        mask: IMask.MaskedRange,
                        from: 0,
                        to: 24
                    },
                    TT: {
                        mask: IMask.MaskedRange,
                        from: 0,
                        to: 59
                    }
                }
            }
        };
    }

    public static IntegerFormatRang(selector, rangoMinimo, rangoMaximo): IMaskFormat {
        return {
            Selector: selector,
            Mask: {
                mask: Number,
                min: rangoMinimo,
                max: rangoMaximo,
                scale: 0,
                signed: false,
                thousandsSeparator: ',',
                padFractionalZeros: false,
                normalizeZeros: true,
                radix: '.',
                mapToRadix: ['.']
            }
        };
    }

    public static DecimalFormatRang(selector, rangoMinimo, rangoMaximo): IMaskFormat {
        return {
            Selector: selector,
            Mask: {
                mask: Number,
                min: rangoMinimo,
                max: rangoMaximo,
                scale: 2,
                signed: false,
                thousandsSeparator: ',',
                padFractionalZeros: false,
                normalizeZeros: true,
                radix: '.',
                mapToRadix: ['.']
            }
        };
    }

    public static Alfanumerico(selector): IMaskFormat {
        return {
            Selector: selector,
            Mask: {   
                mask: /^[a-z0-9]+$/i,
                lazy: false
            }
        };
    }

    public static NumericoSinSignos(selector): IMaskFormat {
        return {
            Selector: selector,
            Mask: {
                mask: /^[0-9]+$/i,
                lazy: false
            }
        };
    }

}

export interface IMaskFormat {
    Selector: string;
    Mask: any | IMask.AnyMaskedOptions | IMask.AnyMaskedOptionsMasked | IMask.MaskedOptions<typeof Masked>;
}