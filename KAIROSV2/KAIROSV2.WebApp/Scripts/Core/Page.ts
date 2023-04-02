import dayjs = require("dayjs");
import { IMaskFormat, MaskFormatsManager } from "../Shared/Utils";
import 'dayjs/locale/es-do';
import { ActionsPermission } from "../Shared/Models/ActionsPermission";
import { HttpFetchService } from "../Shared";

var customParseFormat = require('dayjs/plugin/customParseFormat');

export abstract class Page {
    public MaskManager: MaskFormatsManager;
    public BaseUrl;
    public Permisions: ActionsPermission;

    constructor() {
        dayjs.locale('es-do');
        dayjs.extend(customParseFormat);
        this.AdjustValidation();
    }

    protected RegisterMasks(masks: Array<IMaskFormat>): void {
        if (!this.MaskManager) {
            this.MaskManager = new MaskFormatsManager();
        }

        this.MaskManager.RegisterMasks(masks);
    }

    protected GetPermissions(url: string): void {
        const httpService = new HttpFetchService();
        httpService.Post<ActionsPermission>(url, "", true)
            .then((data) => {
                if (data) {
                    this.Permisions = data;
                }
            }).catch((error) => {
                this.Permisions = new ActionsPermission();
            });
    }

    public Destroy(): void {
        this.MaskManager?.Destroy();
    }

    private AdjustValidation(): void {
        $.validator.methods.range = function (value, element, param) {
            var globalizedValue = value.replace(",", "");
            let result = this.optional(element) || (globalizedValue >= param[0] && globalizedValue <= param[1]);
            return result;
        }
    }
}