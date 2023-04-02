import "jquery-validation";
import "jquery-validation-unobtrusive";

export class JQValidations {

    public static MaxFileSizeValidation(): void {
        if ($.validator.methods['maxfilesize'])
            return;

        $.validator.addMethod('maxfilesize', function (value, element, params) {
            let input = (element as HTMLInputElement);
            let size = params, file = (input.files[0] as File);

            if (file) {
                if ((file.size / 1024) > size) {
                    return false;
                }
            }
            return true;
        });

        $.validator.unobtrusive.adapters.add('maxfilesize', ['size'], function (options) {
            //console.log("registra maxfilesize en unobtrusive");
            options.rules['maxfilesize'] = options.params['size'];
            options.messages['maxfilesize'] = options.message;
        });
    }

    public static AllowedExtensionsValidation(): void {
        if ($.validator.methods['allowedextensions'])
            return;

        $.validator.addMethod('allowedextensions', function (value, element, params) {
            let input = (element as HTMLInputElement);
            let exts = params, file = (input.files[0] as File);
            let extsArray = exts.split(',');

            if (file) {
                if ((new RegExp('(' + extsArray.join('|').replace(/\./g, '\\.') + ')$')).test(value)) {
                    return true;
                }
                return false;
            }
            return true;
        });

        $.validator.unobtrusive.adapters.add('allowedextensions', ['exts'], function (options) {
            options.rules['allowedextensions'] = options.params['exts'];
            options.messages['allowedextensions'] = options.message;
        });
    }

    public static NotEqualValidation(): void {
        if ($.validator.methods['notequal'])
            return;

        $.validator.addMethod('notequal', function (value, element, params) {
            let input = (element as HTMLInputElement);
            let IdName = params, ActualValue = (input.value as string);
            let Id = (<HTMLInputElement>document.getElementById(IdName)).value;

            //console.log("ActualValue: " + ActualValue);
            //console.log("size: " + IdName);
            //console.log("input: " + input);
            //console.log("value: " + value);
            //console.log("Id: " + Id);

            if (ActualValue) {
                if (ActualValue == Id) {
                    return false;
                }
            }
            return true;
        });

        $.validator.unobtrusive.adapters.add('notequal', ['IdinputTocompare'], function (options) {
            options.rules['notequal'] = options.params['IdinputTocompare'];
            options.messages['notequal'] = options.message;
        });
    }
}