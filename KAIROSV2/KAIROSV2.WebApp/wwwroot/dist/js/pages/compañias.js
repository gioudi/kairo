/*! For license information please see compañias.js.LICENSE.txt */
!function(e,t){"object"==typeof exports&&"object"==typeof module?module.exports=t():"function"==typeof define&&define.amd?define([],t):"object"==typeof exports?exports.kairosv2=t():e.kairosv2=t()}(self,(function(){return(()=>{"use strict";var e={"./Scripts/Core/Page.ts":(e,t,a)=>{var o=a("./node_modules/jquery/dist/jquery.js");Object.defineProperty(t,"__esModule",{value:!0}),t.Page=void 0;var i=a("./node_modules/dayjs/dayjs.min.js"),s=a("./Scripts/Shared/Utils/index.ts");a("./node_modules/dayjs/locale/es-do.js");var r=a("./Scripts/Shared/Models/ActionsPermission.ts"),n=a("./Scripts/Shared/index.ts"),c=a("./node_modules/dayjs/plugin/customParseFormat.js"),l=function(){function e(){i.locale("es-do"),i.extend(c),this.AdjustValidation()}return e.prototype.RegisterMasks=function(e){this.MaskManager||(this.MaskManager=new s.MaskFormatsManager),this.MaskManager.RegisterMasks(e)},e.prototype.GetPermissions=function(e){var t=this;(new n.HttpFetchService).Post(e,"",!0).then((function(e){e&&(t.Permisions=e)})).catch((function(e){t.Permisions=new r.ActionsPermission}))},e.prototype.Destroy=function(){var e;null===(e=this.MaskManager)||void 0===e||e.Destroy()},e.prototype.AdjustValidation=function(){o.validator.methods.range=function(e,t,a){var o=e.replace(",","");return this.optional(t)||o>=a[0]&&o<=a[1]}},e}();t.Page=l},"./Scripts/Pages/Compañias/Compañias.ts":function(e,t,a){var o,i=this&&this.__extends||(o=function(e,t){return(o=Object.setPrototypeOf||{__proto__:[]}instanceof Array&&function(e,t){e.__proto__=t}||function(e,t){for(var a in t)Object.prototype.hasOwnProperty.call(t,a)&&(e[a]=t[a])})(e,t)},function(e,t){function a(){this.constructor=e}o(e,t),e.prototype=null===t?Object.create(t):(a.prototype=t.prototype,new a)}),s=this&&this.__awaiter||function(e,t,a,o){return new(a||(a=Promise))((function(i,s){function r(e){try{c(o.next(e))}catch(e){s(e)}}function n(e){try{c(o.throw(e))}catch(e){s(e)}}function c(e){var t;e.done?i(e.value):(t=e.value,t instanceof a?t:new a((function(e){e(t)}))).then(r,n)}c((o=o.apply(e,t||[])).next())}))},r=this&&this.__generator||function(e,t){var a,o,i,s,r={label:0,sent:function(){if(1&i[0])throw i[1];return i[1]},trys:[],ops:[]};return s={next:n(0),throw:n(1),return:n(2)},"function"==typeof Symbol&&(s[Symbol.iterator]=function(){return this}),s;function n(s){return function(n){return function(s){if(a)throw new TypeError("Generator is already executing.");for(;r;)try{if(a=1,o&&(i=2&s[0]?o.return:s[0]?o.throw||((i=o.return)&&i.call(o),0):o.next)&&!(i=i.call(o,s[1])).done)return i;switch(o=0,i&&(s=[2&s[0],i.value]),s[0]){case 0:case 1:i=s;break;case 4:return r.label++,{value:s[1],done:!1};case 5:r.label++,o=s[1],s=[0];continue;case 7:s=r.ops.pop(),r.trys.pop();continue;default:if(!((i=(i=r.trys).length>0&&i[i.length-1])||6!==s[0]&&2!==s[0])){r=0;continue}if(3===s[0]&&(!i||s[1]>i[0]&&s[1]<i[3])){r.label=s[1];break}if(6===s[0]&&r.label<i[1]){r.label=i[1],i=s;break}if(i&&r.label<i[2]){r.label=i[2],r.ops.push(s);break}i[2]&&r.ops.pop(),r.trys.pop();continue}s=t.call(e,r)}catch(e){s=[6,e],o=0}finally{a=i=0}if(5&s[0])throw s[1];return{value:s[0]?s[1]:void 0,done:!0}}([s,n])}}};Object.defineProperty(t,"__esModule",{value:!0}),t["CompañiasPage"]=void 0;var n=a("./Scripts/Shared/index.ts"),c=a("./Scripts/Shared/Components/ConfirmModalMessage.ts"),l=a("./Scripts/Pages/Compañias/GestionCompañias.ts"),d=a("./Scripts/Shared/Utils/JQValidations.ts"),p=a("./Scripts/Shared/Utils/SelectorMenu.ts"),u=function(e){function t(){var t=e.call(this)||this;return t.BaseUrl="/Compañias/Index",t.Init(),t}return i(t,e),t.prototype.Destroy=function(){e.prototype.Destroy.call(this)},t.prototype.Init=function(){this.InicializarButtons(),this.InicializarControls(),e.prototype.GetPermissions.call(this,"/Compañias/ObtenerPermisos")},t.prototype.InicializarControls=function(){var e=this;p.SelectorMenu.SeleccionEnElMenu(),this.gestionCompañias=new l.CompañiasGestionPage(document.getElementById("compañia-modal")),this.gestionCompañias.onCompañiaCreado=function(t){return e.AgregarCompañiaKanba(t)},this.gestionCompañias.onCompañiaActualizado=function(t){return e.ActualizarCompañiaKanba(t)},d.JQValidations.MaxFileSizeValidation(),d.JQValidations.AllowedExtensionsValidation()},t.prototype.InicializarButtons=function(){var e=this,t=document.getElementById("kanba"),a=document.getElementById("compañia-add");t.on("click",".compañia-action",(function(t){t.target.matches(".compañia-edit")?e.EditarCompañia(t.target):t.target.matches(".compañia-delete")&&e.BorrarCompañia(t.target)})),a.addEventListener("click",(function(t){return e.CrearCompañia()}))},t.prototype.BorrarCompañia=function(e){return s(this,void 0,void 0,(function(){var t,a,o=this;return r(this,(function(i){switch(i.label){case 0:return t=e.dataset.compañiaid,a=e.dataset.compañianame,[4,new c.ConfirmModalMessage("Eliminar Compañia","¿Desea eliminar la Compañia "+a+"?","Aceptar","Cancelar").Confirm()];case 1:return i.sent()&&(new n.HttpFetchService).Post("/Compañias/BorrarCompañia",t).then((function(e){e.Result&&o.BorrarCompañiaKanba(t),M.toast({html:e.Message,classes:e.Result?"succes":"error"})})).catch((function(e){M.toast({html:e,classes:"error"})})),[2]}}))}))},t.prototype.CrearCompañia=function(){this.gestionCompañias.NuevaCompañia()},t.prototype.EditarCompañia=function(e){this.gestionCompañias.DatosCompañia(e.dataset.compañiaid,!1)},t.prototype.BorrarCompañiaKanba=function(e){var t=document.getElementById("kanba"),a=document.getElementById(e);t.removeChild(a)},t.prototype.AgregarCompañiaKanba=function(e){var t=document.getElementById("kanba"),a=document.createElement("div");a.setAttribute("id",e.IdCompañia),a.classList.add("col"),a.classList.add("s12"),a.classList.add("m6"),a.classList.add("l4"),a.innerHTML='<div id="profile-card" class="card animate fadeRight"> <div class="card-image waves-effect waves-block waves-light" style="background:lightgrey;"></div><div class="card-content"><a class="btn-floating activator btn-move-up waves-effect waves-light accent-2 z-depth-4 right '+this.DisableAction(this.Permisions.Detalles)+'" style="background-color:#FF6900"><i class="material-icons compañia-action compañia-detail" data-compañiaid="'+e.IdCompañia+'" data-compañianame="'+e.Nombre+'">visibility</i></a><a class="btn-floating btn-move-up waves-effect waves-light accent-2 z-depth-4 right '+this.DisableAction(this.Permisions.Editar)+'" style = "background-color: #162b71"><i class="material-icons compañia-action compañia-edit" data-compañiaid="'+e.IdCompañia+'" data-compañianame="'+e.Nombre+'">edit</i></a><a class="btn-floating btn-move-up waves-effect waves-light grey accent-2 z-depth-4 right '+this.DisableAction(this.Permisions.Borrar)+'"><i class="material-icons compañia-action compañia-delete" data-compañiaid="'+e.IdCompañia+'" data-compañianame="'+e.Nombre+'">delete</i></a><p> <i class="material-icons profile-card-i" style="color:#FF6900">apps</i>'+e.Nombre+'</p><p><i class="material-icons profile-card-i" style="color:#FF6900">select_all</i>ID: '+e.CodigoSICOM+'</p></div><div class="card-reveal"><span class="card-title grey-text text-darken-4">'+e.Nombre+'<i class="material-icons right">close</i></span><p>Id: <b>'+e.IdCompañia+"</b></p><p>Sales Organization: <b>"+e.SalesOrganization+"</b></p><p>Distribution Channel: <b>"+e.DistributionChannel+"</b></p><p>Division: <b>"+e.Division+"</b></p><p>Supplier Type: <b>"+e.SupplierType+"</b></p><p>SICOM: <b>"+e.CodigoSICOM+"</b></p></div>",t.appendChild(a)},t.prototype.ActualizarCompañiaKanba=function(e){document.getElementById(e.IdCompañia).innerHTML='<div id="profile-card" class="card animate fadeRight"><div class="card-image waves-effect waves-block waves-light" style="background:lightgrey;"></div><div class="card-content"><a class="btn-floating activator btn-move-up waves-effect waves-light accent-2 z-depth-4 right '+this.DisableAction(this.Permisions.Detalles)+'" style="background-color:#FF6900"><i class="material-icons compañia-action compañia-detail" data-compañiaid="'+e.IdCompañia+'" data-compañianame="'+e.Nombre+'">visibility</i></a><a class="btn-floating btn-move-up waves-effect waves-light accent-2 z-depth-4 right '+this.DisableAction(this.Permisions.Editar)+'" style = "background-color: #162b71"><i class="material-icons compañia-action compañia-edit" data-compañiaid="'+e.IdCompañia+'" data-compañianame="'+e.Nombre+'">edit</i></a><a class="btn-floating btn-move-up waves-effect waves-light grey accent-2 z-depth-4 right '+this.DisableAction(this.Permisions.Borrar)+'"><i class="material-icons compañia-action compañia-delete" data-compañiaid="'+e.IdCompañia+'" data-compañianame="'+e.Nombre+'">delete</i></a><p> <i class="material-icons profile-card-i" style="color:#FF6900">apps</i>'+e.Nombre+'</p><p><i class="material-icons profile-card-i" style="color:#FF6900">select_all</i>ID: '+e.CodigoSICOM+'</p></div><div class="card-reveal"><span class="card-title grey-text text-darken-4">'+e.Nombre+'<i class="material-icons right">close</i></span><p>Id: <b>'+e.IdCompañia+"</b></p><p>Sales Organization: <b>"+e.SalesOrganization+"</b></p><p>Distribution Channel: <b>"+e.DistributionChannel+"</b></p><p>Division: <b>"+e.Division+"</b></p><p>Supplier Type: <b>"+e.SupplierType+"</b></p><p>SICOM: <b>"+e.CodigoSICOM+"</b></p></div>"},t.prototype.DisableAction=function(e){return e?"":"disable-action"},t}(a("./Scripts/Core/Page.ts").Page);t["CompañiasPage"]=u;var m=new u;document.addEventListener("turbolinks:render",(function(e){-1!=decodeURI(document.URL).indexOf("/Compañias/Index")?m?m.Init():m=new u:null==m||m.Destroy()}))},"./Scripts/Pages/Compañias/GestionCompañias.ts":(e,t,a)=>{var o=a("./node_modules/jquery/dist/jquery.js");Object.defineProperty(t,"__esModule",{value:!0}),t["CompañiasGestionPage"]=void 0;var i=a("./Scripts/Shared/index.ts"),s=a("./Scripts/Shared/Models/Compañia.ts"),r=function(){function e(e){this._baseUrl="/Compañias",this._httpService=new i.HttpFetchService,this._modalBase=e,this.InicializarModal()}return e.prototype.NuevaCompañia=function(){var e=this;this._httpService.Post(this._baseUrl+"/NuevaCompañia",null,!1).then((function(t){e._modalBase.innerHTML=t,e.InicilizarFormCompañia(!0)})),this._modalInstance.open()},e.prototype.GuardarCompañia=function(){var e,t=this,a=null===(e=this._formData.get("RequestVerificationToken"))||void 0===e?void 0:e.toString();this._httpService.PostForm(this._baseUrl+"/CrearCompañia",this._formData,a).then((function(e){e.Result&&(t.LimpiarFormulario(),t.onCompañiaCreado&&t.onCompañiaCreado(t.ExtraerCompañia())),M.toast({html:e.Message,classes:e.Result?"succes":"error"})})).catch((function(e){return console.log(e)}))},e.prototype.DatosCompañia=function(e,t){var a=this,o={compañia:e,lectura:t};this._compañiaIdActualizar=e,this._httpService.Post(this._baseUrl+"/DatosCompañia",o,!1).then((function(e){a._modalBase.innerHTML=e,a.InicilizarFormCompañia(!1)})),this._modalInstance.open()},e.prototype.ActualizarCompañia=function(){var e,t=this,a=null===(e=this._formData.get("RequestVerificationToken"))||void 0===e?void 0:e.toString();this._formData.set("IdCompañia",this._compañiaIdActualizar),this._httpService.PostForm(this._baseUrl+"/ActualizarCompañia",this._formData,a).then((function(e){e.Result&&(t.onCompañiaActualizado&&t.onCompañiaActualizado(t.ExtraerCompañia()),t.LimpiarFormulario(),t.CerrarModal()),M.toast({html:e.Message,classes:e.Result?"succes":"error"})})).catch((function(e){return console.log(e)}))},e.prototype.InicializarModal=function(){this._modalInstance=M.Modal.init(this._modalBase,{dismissible:!1,opacity:.5,inDuration:300,outDuration:200,startingTop:"6%",endingTop:"8%"})},e.prototype.InicilizarFormCompañia=function(e){var t=this;M.updateTextFields(),this._formCompañia=document.querySelector("#compañia-form"),this._formData=new FormData(this._formCompañia),o(this._formCompañia).removeData("validator").removeData("unobtrusiveValidation"),o.validator.unobtrusive.parse(this._formCompañia),document.getElementById("compañia-modal-cancel").addEventListener("click",(function(e){return t.CerrarModal()})),this._formCompañia.onsubmit=function(a){return a.preventDefault(),o(t._formCompañia).valid()&&(t._formData=new FormData(t._formCompañia),e?t.GuardarCompañia():t.ActualizarCompañia()),!1}},e.prototype.ExtraerCompañia=function(){var e,t,a,o,i,r,n,c=new s.Compañia;return c.IdCompañia=null===(e=this._formData.get("IdCompañia"))||void 0===e?void 0:e.toString(),c.Nombre=null===(t=this._formData.get("Compañia"))||void 0===t?void 0:t.toString(),c.SalesOrganization=null===(a=this._formData.get("SalesOrganization"))||void 0===a?void 0:a.toString(),c.DistributionChannel=null===(o=this._formData.get("DistributionChannel"))||void 0===o?void 0:o.toString(),c.Division=null===(i=this._formData.get("Division"))||void 0===i?void 0:i.toString(),c.SupplierType=null===(r=this._formData.get("SupplierType"))||void 0===r?void 0:r.toString(),c.CodigoSICOM=null===(n=this._formData.get("CodigoSICOM"))||void 0===n?void 0:n.toString(),c},e.prototype.LimpiarFormulario=function(){this._formCompañia.reset(),o("#compañia-id").val(null).trigger("change"),M.updateTextFields()},e.prototype.CerrarModal=function(){this._modalInstance.close(),this._formCompañia.parentNode.removeChild(this._formCompañia),this._compañiaIdActualizar=""},e}();t["CompañiasGestionPage"]=r},"./Scripts/Shared/Components/ConfirmModalMessage.ts":function(e,t){var a=this&&this.__awaiter||function(e,t,a,o){return new(a||(a=Promise))((function(i,s){function r(e){try{c(o.next(e))}catch(e){s(e)}}function n(e){try{c(o.throw(e))}catch(e){s(e)}}function c(e){var t;e.done?i(e.value):(t=e.value,t instanceof a?t:new a((function(e){e(t)}))).then(r,n)}c((o=o.apply(e,t||[])).next())}))},o=this&&this.__generator||function(e,t){var a,o,i,s,r={label:0,sent:function(){if(1&i[0])throw i[1];return i[1]},trys:[],ops:[]};return s={next:n(0),throw:n(1),return:n(2)},"function"==typeof Symbol&&(s[Symbol.iterator]=function(){return this}),s;function n(s){return function(n){return function(s){if(a)throw new TypeError("Generator is already executing.");for(;r;)try{if(a=1,o&&(i=2&s[0]?o.return:s[0]?o.throw||((i=o.return)&&i.call(o),0):o.next)&&!(i=i.call(o,s[1])).done)return i;switch(o=0,i&&(s=[2&s[0],i.value]),s[0]){case 0:case 1:i=s;break;case 4:return r.label++,{value:s[1],done:!1};case 5:r.label++,o=s[1],s=[0];continue;case 7:s=r.ops.pop(),r.trys.pop();continue;default:if(!((i=(i=r.trys).length>0&&i[i.length-1])||6!==s[0]&&2!==s[0])){r=0;continue}if(3===s[0]&&(!i||s[1]>i[0]&&s[1]<i[3])){r.label=s[1];break}if(6===s[0]&&r.label<i[1]){r.label=i[1],i=s;break}if(i&&r.label<i[2]){r.label=i[2],r.ops.push(s);break}i[2]&&r.ops.pop(),r.trys.pop();continue}s=t.call(e,r)}catch(e){s=[6,e],o=0}finally{a=i=0}if(5&s[0])throw s[1];return{value:s[0]?s[1]:void 0,done:!0}}([s,n])}}};Object.defineProperty(t,"__esModule",{value:!0}),t.ConfirmModalMessage=void 0;var i=function(){function e(e,t,a,o){this.title=e,this.message=t,this.acceptText=a,this.dismissText=o,this.idMesssage="confirm-message",this.parent=document.body,this.CreateModal(),this.AppendModel()}return e.prototype.CreateModal=function(){this.modal=document.createElement("div"),this.modal.id=this.idMesssage,this.modal.classList.add("modal","modal-fixed-footer","modal-confirm-message-small");var e=document.createElement("div");e.classList.add("modal-content"),this.modal.appendChild(e);var t=document.createElement("h5");t.textContent=this.title,e.appendChild(t);var a=document.createElement("p");a.textContent=this.message,e.appendChild(a);var o=document.createElement("div");o.classList.add("modal-footer"),this.modal.appendChild(o),this.dismissButton=document.createElement("button"),this.dismissButton.classList.add("modal-action","waves-effect","waves-red","btn-flat","btn-orange"),this.dismissButton.type="button",this.dismissButton.textContent=this.dismissText,o.appendChild(this.dismissButton),this.acceptButton=document.createElement("button"),this.acceptButton.classList.add("modal-action","waves-effect","waves-red","btn-flat","btn-orange"),this.acceptButton.type="button",this.acceptButton.textContent=this.acceptText,o.appendChild(this.acceptButton)},e.prototype.AppendModel=function(){this.parent.appendChild(this.modal)},e.prototype.destroy=function(){this.modalInstance&&(this.modalInstance.close(),this.modalInstance.destroy());var e=document.querySelector("#"+this.idMesssage);e.parentNode.removeChild(e)},e.prototype.Confirm=function(){return a(this,void 0,void 0,(function(){var e=this;return o(this,(function(t){return[2,new Promise((function(t,a){(!e.modal||!e.acceptButton||!e.dismissButton)&&(e.destroy(),a("Fallo algo en la creación del mensaje")),M.Modal.init(e.modal,{dismissible:!1,opacity:.5,inDuration:300,outDuration:200,startingTop:"6%",endingTop:"8%"}),e.modalInstance=M.Modal.getInstance(e.modal),e.modalInstance.open(),e.acceptButton.addEventListener("click",(function(){t(!0),e.destroy()})),e.dismissButton.addEventListener("click",(function(){t(!1),e.destroy()}))}))]}))}))},e}();t.ConfirmModalMessage=i},"./Scripts/Shared/Http/HttpFetchService.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.HttpFetchService=void 0;var a=function(){function e(){}return e.prototype.Post=function(e,t,a){return void 0===a&&(a=!0),new Promise((function(o,i){fetch(e,{method:"post",headers:{"Content-Type":"application/json"},body:JSON.stringify(t)}).then((function(e){return a?e.json():e.text()})).then((function(e){o(e)})).catch((function(e){console.log(e),i(e)}))}))},e.prototype.PostForm=function(e,t,a,o){return void 0===o&&(o=!0),new Promise((function(i,s){fetch(e,{method:"post",headers:{RequestVerificationToken:a},body:t}).then((function(e){return o?e.json():e.text()})).then((function(e){i(e)})).catch((function(e){console.log(e),s(e)}))}))},e.prototype.PostFormURL=function(e,t,a){return void 0===a&&(a=!0),new Promise((function(o,i){fetch(e,{method:"post",headers:{"Content-Type":"application/json"},body:t}).then((function(e){return a?e.json():e.text()})).then((function(e){o(e)})).catch((function(e){console.log(e),i(e)}))}))},e}();t.HttpFetchService=a},"./Scripts/Shared/Http/index.ts":function(e,t,a){var o=this&&this.__createBinding||(Object.create?function(e,t,a,o){void 0===o&&(o=a),Object.defineProperty(e,o,{enumerable:!0,get:function(){return t[a]}})}:function(e,t,a,o){void 0===o&&(o=a),e[o]=t[a]}),i=this&&this.__exportStar||function(e,t){for(var a in e)"default"===a||Object.prototype.hasOwnProperty.call(t,a)||o(t,e,a)};Object.defineProperty(t,"__esModule",{value:!0}),t.services=void 0;var s=a("./Scripts/Shared/Http/HttpFetchService.ts");t.services=[s.HttpFetchService],i(a("./Scripts/Shared/Http/HttpFetchService.ts"),t)},"./Scripts/Shared/Models/ActionsPermission.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.ActionsPermission=void 0;t.ActionsPermission=function(){}},"./Scripts/Shared/Models/Area.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Area=void 0;t.Area=function(){}},"./Scripts/Shared/Models/Cabezote.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Cabezote=void 0;t.Cabezote=function(){}},"./Scripts/Shared/Models/Compañia.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t["Compañia"]=void 0;t["Compañia"]=function(){}},"./Scripts/Shared/Models/Conductor.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Conductor=void 0;t.Conductor=function(){}},"./Scripts/Shared/Models/Contador.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Contador=void 0;t.Contador=function(){}},"./Scripts/Shared/Models/Despacho.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Despacho=void 0;t.Despacho=function(){}},"./Scripts/Shared/Models/FechasCorteDTO.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.FechasCorteDTO=void 0;t.FechasCorteDTO=function(){}},"./Scripts/Shared/Models/Linea.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Linea=void 0;t.Linea=function(){}},"./Scripts/Shared/Models/Log.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Log=void 0;t.Log=function(){}},"./Scripts/Shared/Models/MessageResponse.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0})},"./Scripts/Shared/Models/ProcesamientoArchivos.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Paso2Mapeo=t.Paso1Mapeo=t.MapeoArchivos=void 0;t.MapeoArchivos=function(){};t.Paso1Mapeo=function(){};t.Paso2Mapeo=function(){}},"./Scripts/Shared/Models/Producto.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Producto=void 0;t.Producto=function(){}},"./Scripts/Shared/Models/Proveedor.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Proveedor=void 0;t.Proveedor=function(){}},"./Scripts/Shared/Models/Rol.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Rol=void 0;t.Rol=function(){}},"./Scripts/Shared/Models/Tanque.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Tanque=void 0;t.Tanque=function(){}},"./Scripts/Shared/Models/Terminal.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Terminal=void 0;t.Terminal=function(){}},"./Scripts/Shared/Models/Trailer.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Trailer=void 0;t.Trailer=function(){}},"./Scripts/Shared/Models/Usuario.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Usuario=void 0;t.Usuario=function(){}},"./Scripts/Shared/Models/index.ts":function(e,t,a){var o=this&&this.__createBinding||(Object.create?function(e,t,a,o){void 0===o&&(o=a),Object.defineProperty(e,o,{enumerable:!0,get:function(){return t[a]}})}:function(e,t,a,o){void 0===o&&(o=a),e[o]=t[a]}),i=this&&this.__exportStar||function(e,t){for(var a in e)"default"===a||Object.prototype.hasOwnProperty.call(t,a)||o(t,e,a)};Object.defineProperty(t,"__esModule",{value:!0}),t.models=void 0;var s=a("./Scripts/Shared/Models/Usuario.ts"),r=a("./Scripts/Shared/Models/Cabezote.ts"),n=a("./Scripts/Shared/Models/Trailer.ts"),c=a("./Scripts/Shared/Models/Conductor.ts"),l=a("./Scripts/Shared/Models/Area.ts"),d=a("./Scripts/Shared/Models/Compañia.ts"),p=a("./Scripts/Shared/Models/Log.ts"),u=a("./Scripts/Shared/Models/Linea.ts"),m=a("./Scripts/Shared/Models/Rol.ts"),h=a("./Scripts/Shared/Models/Terminal.ts"),f=a("./Scripts/Shared/Models/Tanque.ts"),v=a("./Scripts/Shared/Models/Contador.ts"),S=a("./Scripts/Shared/Models/Proveedor.ts"),M=a("./Scripts/Shared/Models/ProcesamientoArchivos.ts"),b=a("./Scripts/Shared/Models/Producto.ts"),y=a("./Scripts/Shared/Models/Despacho.ts"),g=a("./Scripts/Shared/Models/FechasCorteDTO.ts");t.models=[s.Usuario,r.Cabezote,n.Trailer,c.Conductor,l.Area,p.Log,m.Rol,d.Compañia,h.Terminal,f.Tanque,v.Contador,u.Linea,S.Proveedor,M.MapeoArchivos,b.Producto,y.Despacho,g.FechasCorteDTO],i(a("./Scripts/Shared/Models/MessageResponse.ts"),t),i(a("./Scripts/Shared/Models/Usuario.ts"),t),i(a("./Scripts/Shared/Models/Cabezote.ts"),t),i(a("./Scripts/Shared/Models/Trailer.ts"),t),i(a("./Scripts/Shared/Models/Conductor.ts"),t),i(a("./Scripts/Shared/Models/Area.ts"),t),i(a("./Scripts/Shared/Models/Linea.ts"),t),i(a("./Scripts/Shared/Models/Compañia.ts"),t),i(a("./Scripts/Shared/Models/Rol.ts"),t),i(a("./Scripts/Shared/Models/Terminal.ts"),t),i(a("./Scripts/Shared/Models/Tanque.ts"),t),i(a("./Scripts/Shared/Models/Contador.ts"),t),i(a("./Scripts/Shared/Models/Log.ts"),t),i(a("./Scripts/Shared/Models/Proveedor.ts"),t),i(a("./Scripts/Shared/Models/ProcesamientoArchivos.ts"),t),i(a("./Scripts/Shared/Models/Producto.ts"),t),i(a("./Scripts/Shared/Models/Despacho.ts"),t),i(a("./Scripts/Shared/Models/FechasCorteDTO.ts"),t)},"./Scripts/Shared/Utils/HTMLExtensions.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),HTMLElement.prototype.on=function(e,t,a){this.addEventListener(e,(function(e){e.target.matches(t)&&a(e)}))}},"./Scripts/Shared/Utils/JQValidations.ts":(e,t,a)=>{var o=a("./node_modules/jquery/dist/jquery.js");Object.defineProperty(t,"__esModule",{value:!0}),t.JQValidations=void 0,a("./node_modules/jquery-validation/dist/jquery.validate.js"),a("./node_modules/jquery-validation-unobtrusive/dist/jquery.validate.unobtrusive.js");var i=function(){function e(){}return e.MaxFileSizeValidation=function(){o.validator.methods.maxfilesize||(o.validator.addMethod("maxfilesize",(function(e,t,a){var o=a,i=t.files[0];return!(i&&i.size/1024>o)})),o.validator.unobtrusive.adapters.add("maxfilesize",["size"],(function(e){e.rules.maxfilesize=e.params.size,e.messages.maxfilesize=e.message})))},e.AllowedExtensionsValidation=function(){o.validator.methods.allowedextensions||(o.validator.addMethod("allowedextensions",(function(e,t,a){var o=a,i=t.files[0],s=o.split(",");return!i||!!new RegExp("("+s.join("|").replace(/\./g,"\\.")+")$").test(e)})),o.validator.unobtrusive.adapters.add("allowedextensions",["exts"],(function(e){e.rules.allowedextensions=e.params.exts,e.messages.allowedextensions=e.message})))},e.NotEqualValidation=function(){o.validator.methods.notequal||(o.validator.addMethod("notequal",(function(e,t,a){var o=a,i=t.value,s=document.getElementById(o).value;return!i||i!=s})),o.validator.unobtrusive.adapters.add("notequal",["IdinputTocompare"],(function(e){e.rules.notequal=e.params.IdinputTocompare,e.messages.notequal=e.message})))},e}();t.JQValidations=i},"./Scripts/Shared/Utils/MaskFormats.ts":(e,t,a)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.MaskFormats=void 0;var o=a("./node_modules/imask/esm/index.js"),i=function(){function e(){}return e.IntegerFormat=function(){return{Selector:".formato-int",Mask:{mask:Number,scale:0,signed:!1,thousandsSeparator:",",padFractionalZeros:!1,normalizeZeros:!0,radix:".",mapToRadix:["."]}}},e.DecimalFormat=function(){return{Selector:".formato-decimal",Mask:{mask:Number,scale:2,signed:!1,thousandsSeparator:",",padFractionalZeros:!1,normalizeZeros:!0,radix:".",mapToRadix:["."]}}},e.DateShortFormat=function(){return{Selector:".formato-fecha-corta",Mask:{mask:"00{/}MMM{/}0000",overwrite:!0,autofix:!0,blocks:{MMM:{mask:o.default.MaskedEnum,enum:["ene","feb","mar","abr","may","jun","jul","ago","sep","oct","nov","dic"]}}}}},e.DateLongFormat=function(){return{Selector:".formato-fecha-larga",Mask:{mask:"00{/}MMM{/}0000{ }HH{:}TT{:}TT",overwrite:!0,autofix:!0,blocks:{MMM:{mask:o.default.MaskedEnum,enum:["ene","feb","mar","abr","may","jun","jul","ago","sep","oct","nov","dic"]},HH:{mask:o.default.MaskedRange,from:0,to:24},TT:{mask:o.default.MaskedRange,from:0,to:59}}}}},e.IntegerFormatRang=function(e,t,a){return{Selector:e,Mask:{mask:Number,min:t,max:a,scale:0,signed:!1,thousandsSeparator:",",padFractionalZeros:!1,normalizeZeros:!0,radix:".",mapToRadix:["."]}}},e.DecimalFormatRang=function(e,t,a){return{Selector:e,Mask:{mask:Number,min:t,max:a,scale:2,signed:!1,thousandsSeparator:",",padFractionalZeros:!1,normalizeZeros:!0,radix:".",mapToRadix:["."]}}},e.Alfanumerico=function(e){return{Selector:e,Mask:{mask:/^[a-z0-9]+$/i,lazy:!1}}},e.NumericoSinSignos=function(e){return{Selector:e,Mask:{mask:/^[0-9]+$/i,lazy:!1}}},e}();t.MaskFormats=i},"./Scripts/Shared/Utils/MaskFormatsManager.ts":(e,t,a)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.MaskFormatsManager=void 0;var o=a("./node_modules/imask/esm/index.js"),i=function(){function e(){this.FormatElements=new Array}return e.prototype.RegisterMasks=function(e){this.CleanAll(),this.Masks=e},e.prototype.ApplyMasks=function(){var e,t=this;this.CleanFormats(),null===(e=this.Masks)||void 0===e||e.forEach((function(e,a,o){t.ApplyFormat(e)}))},e.prototype.AddApplyMask=function(e){this.Masks.push(e),this.ApplyFormat(e)},e.prototype.UnmaskFormats=function(){var e;null===(e=this.FormatElements)||void 0===e||e.forEach((function(e,t,a){e.MaskInput.el.input.value=e.MaskInput.unmaskedValue,e.MaskInput.masked.reset()}))},e.prototype.SetUnmaskedFormValue=function(e){var t;null===(t=this.FormatElements)||void 0===t||t.forEach((function(t,a,o){e.set(t.MaskInput.el.input.name,t.MaskInput.unmaskedValue),t.MaskInput.masked.reset()}))},e.prototype.UpdateValue=function(e){var t=this;(null==e?void 0:e.length)>0&&e.forEach((function(e){var a=t.FormatElements.filter((function(t){return t.Selector===e}));(null==a?void 0:a.length)>0&&a.forEach((function(e){e.MaskInput.updateValue(),e.MaskInput.unmaskedValue=e.MaskInput.el.value}))}))},e.prototype.ApplyFormat=function(e){var t=this;document.querySelectorAll(e.Selector).forEach((function(a){t.FormatElements.push({Selector:e.Selector,MaskInput:o.default(a,e.Mask)})}))},e.prototype.CleanAll=function(){var e;(null===(e=this.Masks)||void 0===e?void 0:e.length)>0&&this.Masks.forEach((function(e,t,a){a.splice(t,1)})),this.CleanFormats()},e.prototype.CleanFormats=function(){var e;(null===(e=this.FormatElements)||void 0===e?void 0:e.length)>0&&this.FormatElements.forEach((function(e,t,a){e.MaskInput.masked.reset(),e.MaskInput.destroy,a.splice(t,1)}))},e.prototype.Destroy=function(){this.CleanAll(),this.Masks=null,this.FormatElements=null},e}();t.MaskFormatsManager=i},"./Scripts/Shared/Utils/SelectorMenu.ts":(e,t,a)=>{var o=a("./node_modules/jquery/dist/jquery.js");Object.defineProperty(t,"__esModule",{value:!0}),t.SelectorMenu=void 0;var i=function(){function e(){}return e.SeleccionEnElMenu=function(){var e=location.href,t=e.indexOf("/",8),a="a[href$='"+e.substr(t,e.length)+"']";o(".nav-list li a").removeClass("active"),o(".collapsible-body").css({display:""}),o(".listaPadre").removeClass("active"),o(a).addClass("active");var i=o(a).attr("name");o("#"+i).addClass("active"),o("#"+i+" .collapsible-body").css({display:"block"});var s=o("#"+i).attr("name");o("#"+s).addClass("active"),o("#"+s+" #"+i+" .collapsible-body").css({display:"block"}),o("#"+s+" .nivel2").css({display:"block"}),o("#"+s+" .collapsible-body").first().css({display:"block"})},e}();t.SelectorMenu=i},"./Scripts/Shared/Utils/index.ts":function(e,t,a){var o=this&&this.__createBinding||(Object.create?function(e,t,a,o){void 0===o&&(o=a),Object.defineProperty(e,o,{enumerable:!0,get:function(){return t[a]}})}:function(e,t,a,o){void 0===o&&(o=a),e[o]=t[a]}),i=this&&this.__exportStar||function(e,t){for(var a in e)"default"===a||Object.prototype.hasOwnProperty.call(t,a)||o(t,e,a)};Object.defineProperty(t,"__esModule",{value:!0}),t.components=void 0;var s=a("./Scripts/Shared/Utils/JQValidations.ts"),r=a("./Scripts/Shared/Utils/MaskFormats.ts"),n=a("./Scripts/Shared/Utils/MaskFormatsManager.ts");t.components=[s.JQValidations,r.MaskFormats,n.MaskFormatsManager],i(a("./Scripts/Shared/Utils/JQValidations.ts"),t),i(a("./Scripts/Shared/Utils/MaskFormats.ts"),t),i(a("./Scripts/Shared/Utils/MaskFormatsManager.ts"),t)},"./Scripts/Shared/index.ts":function(e,t,a){var o=this&&this.__createBinding||(Object.create?function(e,t,a,o){void 0===o&&(o=a),Object.defineProperty(e,o,{enumerable:!0,get:function(){return t[a]}})}:function(e,t,a,o){void 0===o&&(o=a),e[o]=t[a]}),i=this&&this.__exportStar||function(e,t){for(var a in e)"default"===a||Object.prototype.hasOwnProperty.call(t,a)||o(t,e,a)};Object.defineProperty(t,"__esModule",{value:!0}),i(a("./Scripts/Shared/Http/index.ts"),t),i(a("./Scripts/Shared/Models/index.ts"),t),i(a("./Scripts/Shared/Utils/HTMLExtensions.ts"),t)}},t={};function a(o){if(t[o])return t[o].exports;var i=t[o]={exports:{}};return e[o].call(i.exports,i,i.exports,a),i.exports}return a.m=e,a.x=e=>{},a.d=(e,t)=>{for(var o in t)a.o(t,o)&&!a.o(e,o)&&Object.defineProperty(e,o,{enumerable:!0,get:t[o]})},a.o=(e,t)=>Object.prototype.hasOwnProperty.call(e,t),a.r=e=>{"undefined"!=typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(e,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(e,"__esModule",{value:!0})},(()=>{var e={compañias:0},t=[["./Scripts/Pages/Compañias/Compañias.ts","packages"]],o=e=>{},i=(i,s)=>{for(var r,n,[c,l,d,p]=s,u=0,m=[];u<c.length;u++)n=c[u],a.o(e,n)&&e[n]&&m.push(e[n][0]),e[n]=0;for(r in l)a.o(l,r)&&(a.m[r]=l[r]);for(d&&d(a),i&&i(s);m.length;)m.shift()();return p&&t.push.apply(t,p),o()},s=self.webpackChunkkairosv2=self.webpackChunkkairosv2||[];function r(){for(var o,i=0;i<t.length;i++){for(var s=t[i],r=!0,n=1;n<s.length;n++){var c=s[n];0!==e[c]&&(r=!1)}r&&(t.splice(i--,1),o=a(a.s=s[0]))}return 0===t.length&&(a.x(),a.x=e=>{}),o}s.forEach(i.bind(null,0)),s.push=i.bind(null,s.push.bind(s));var n=a.x;a.x=()=>(a.x=n||(e=>{}),(o=r)())})(),a.x()})()}));
//# sourceMappingURL=compañias.js.map