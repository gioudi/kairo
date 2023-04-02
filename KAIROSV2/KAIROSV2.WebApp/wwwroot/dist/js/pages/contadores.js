/*! For license information please see contadores.js.LICENSE.txt */
!function(e,t){"object"==typeof exports&&"object"==typeof module?module.exports=t():"function"==typeof define&&define.amd?define([],t):"object"==typeof exports?exports.kairosv2=t():e.kairosv2=t()}(self,(function(){return(()=>{"use strict";var e={"./Scripts/Core/Page.ts":(e,t,o)=>{var r=o("./node_modules/jquery/dist/jquery.js");Object.defineProperty(t,"__esModule",{value:!0}),t.Page=void 0;var a=o("./node_modules/dayjs/dayjs.min.js"),n=o("./Scripts/Shared/Utils/index.ts");o("./node_modules/dayjs/locale/es-do.js");var s=o("./Scripts/Shared/Models/ActionsPermission.ts"),i=o("./Scripts/Shared/index.ts"),d=o("./node_modules/dayjs/plugin/customParseFormat.js"),l=function(){function e(){a.locale("es-do"),a.extend(d),this.AdjustValidation()}return e.prototype.RegisterMasks=function(e){this.MaskManager||(this.MaskManager=new n.MaskFormatsManager),this.MaskManager.RegisterMasks(e)},e.prototype.GetPermissions=function(e){var t=this;(new i.HttpFetchService).Post(e,"",!0).then((function(e){e&&(t.Permisions=e)})).catch((function(e){t.Permisions=new s.ActionsPermission}))},e.prototype.Destroy=function(){var e;null===(e=this.MaskManager)||void 0===e||e.Destroy()},e.prototype.AdjustValidation=function(){r.validator.methods.range=function(e,t,o){var r=e.replace(",","");return this.optional(t)||r>=o[0]&&r<=o[1]}},e}();t.Page=l},"./Scripts/Pages/Contadores/Contadores.ts":function(e,t,o){var r,a=o("./node_modules/jquery/dist/jquery.js"),n=this&&this.__extends||(r=function(e,t){return(r=Object.setPrototypeOf||{__proto__:[]}instanceof Array&&function(e,t){e.__proto__=t}||function(e,t){for(var o in t)Object.prototype.hasOwnProperty.call(t,o)&&(e[o]=t[o])})(e,t)},function(e,t){function o(){this.constructor=e}r(e,t),e.prototype=null===t?Object.create(t):(o.prototype=t.prototype,new o)}),s=this&&this.__awaiter||function(e,t,o,r){return new(o||(o=Promise))((function(a,n){function s(e){try{d(r.next(e))}catch(e){n(e)}}function i(e){try{d(r.throw(e))}catch(e){n(e)}}function d(e){var t;e.done?a(e.value):(t=e.value,t instanceof o?t:new o((function(e){e(t)}))).then(s,i)}d((r=r.apply(e,t||[])).next())}))},i=this&&this.__generator||function(e,t){var o,r,a,n,s={label:0,sent:function(){if(1&a[0])throw a[1];return a[1]},trys:[],ops:[]};return n={next:i(0),throw:i(1),return:i(2)},"function"==typeof Symbol&&(n[Symbol.iterator]=function(){return this}),n;function i(n){return function(i){return function(n){if(o)throw new TypeError("Generator is already executing.");for(;s;)try{if(o=1,r&&(a=2&n[0]?r.return:n[0]?r.throw||((a=r.return)&&a.call(r),0):r.next)&&!(a=a.call(r,n[1])).done)return a;switch(r=0,a&&(n=[2&n[0],a.value]),n[0]){case 0:case 1:a=n;break;case 4:return s.label++,{value:n[1],done:!1};case 5:s.label++,r=n[1],n=[0];continue;case 7:n=s.ops.pop(),s.trys.pop();continue;default:if(!((a=(a=s.trys).length>0&&a[a.length-1])||6!==n[0]&&2!==n[0])){s=0;continue}if(3===n[0]&&(!a||n[1]>a[0]&&n[1]<a[3])){s.label=n[1];break}if(6===n[0]&&s.label<a[1]){s.label=a[1],a=n;break}if(a&&s.label<a[2]){s.label=a[2],s.ops.push(n);break}a[2]&&s.ops.pop(),s.trys.pop();continue}n=t.call(e,s)}catch(e){n=[6,e],r=0}finally{o=a=0}if(5&n[0])throw n[1];return{value:n[0]?n[1]:void 0,done:!0}}([n,i])}}};Object.defineProperty(t,"__esModule",{value:!0}),t.ContadoresPage=void 0;var d=o("./Scripts/Shared/index.ts"),l=o("./Scripts/Shared/Components/ConfirmModalMessage.ts"),c=o("./Scripts/Shared/Components/ConfigureDataTable.ts"),u=o("./Scripts/Pages/Contadores/GestionContadores.ts"),p=o("./Scripts/Shared/Utils/SelectorMenu.ts"),f=o("./Scripts/Shared/Utils/index.ts"),h=function(e){function t(){var t=e.call(this)||this;return t.BaseUrl="/Contadores/Index",t._maskManager=new f.MaskFormatsManager,t.Init(),t}return n(t,e),t.prototype.Destroy=function(){e.prototype.Destroy.call(this),this.Table.destroy(!1)},t.prototype.Init=function(){this.InicializarButtons(),this.InicializarControls(),e.prototype.GetPermissions.call(this,"/Contadores/ObtenerPermisos")},t.prototype.InicializarControls=function(){var e=this;p.SelectorMenu.SeleccionEnElMenu(),this.Table=(new c.ConfigureDataTable).Configure("#data-table-contador",[{width:"5%",targets:0}]),this.gestionContadores=new u.ContadoresGestionPage(document.getElementById("contador-modal")),this.gestionContadores.onContadorCreado=function(t){return e.AgregarFilaDatatable(t)}},t.prototype.InicializarButtons=function(){var e=this,t=document.getElementById("data-table-contador"),o=document.getElementById("contador-add");t.on("click",".contador-action",(function(t){t.target.matches(".contador-delete")&&e.BorrarContador(t.target)})),o.addEventListener("click",(function(t){return e.CrearContador()}))},t.prototype.BorrarContador=function(e){return s(this,void 0,void 0,(function(){var t,o,r=this;return i(this,(function(a){switch(a.label){case 0:return t=e.dataset.contadorid,o={Contador:t},[4,new l.ConfirmModalMessage("Eliminar Contador","¿Desea eliminar el Contador "+t+"?","Aceptar","Cancelar").Confirm()];case 1:return a.sent()&&(new d.HttpFetchService).Post("/Contadores/BorrarContador",o).then((function(t){t.Result&&r.BorrarFilaDatatable(e),M.toast({html:t.Message,classes:t.Result?"succes":"error"})})).catch((function(e){M.toast({html:e,classes:"error"})})),[2]}}))}))},t.prototype.CrearContador=function(){this.gestionContadores.NuevoContador()},t.prototype.BorrarFilaDatatable=function(e){var t=a(e).closest("tr");if(t.prev().hasClass("parent")){var o=this.Table.row(t.prev());o.remove(),o.draw()}var r=this.Table.row(t);r.remove(),r.draw()},t.prototype.AgregarFilaDatatable=function(e){var t=this.Table.row.add([e.IdContador,'<a class="cursor-point '+this.DisableAction(this.Permisions.Borrar)+'" data-turbolinks="false"><i class="material-icons contador-action contador-delete "data-contadorid="'+e.IdContador+'">delete</i></a>']);this.Table.row(t).node().id=e.IdContador,t.draw(!1)},t.prototype.DisableAction=function(e){return e?"":"disable-action"},t}(o("./Scripts/Core/Page.ts").Page);t.ContadoresPage=h;var m=new h;document.addEventListener("turbolinks:render",(function(e){-1!=document.URL.indexOf("/Contadores/Index")?m?m.Init():m=new h:null==m||m.Destroy()}))},"./Scripts/Pages/Contadores/GestionContadores.ts":function(e,t,o){var r,a=o("./node_modules/jquery/dist/jquery.js"),n=this&&this.__extends||(r=function(e,t){return(r=Object.setPrototypeOf||{__proto__:[]}instanceof Array&&function(e,t){e.__proto__=t}||function(e,t){for(var o in t)Object.prototype.hasOwnProperty.call(t,o)&&(e[o]=t[o])})(e,t)},function(e,t){function o(){this.constructor=e}r(e,t),e.prototype=null===t?Object.create(t):(o.prototype=t.prototype,new o)});Object.defineProperty(t,"__esModule",{value:!0}),t.ContadoresGestionPage=void 0;var s=o("./Scripts/Shared/index.ts");o("./node_modules/jquery-qubit/jquery.qubit.js"),o("./node_modules/jquery-bonsai/jquery.bonsai.js"),o("./node_modules/jquery-bonsai/jquery.bonsai.css"),o("./node_modules/select2/dist/js/select2.js"),o("./node_modules/select2/dist/css/select2.css");var i=o("./Scripts/Shared/Utils/MaskFormats.ts"),d=function(e){function t(t){var o=e.call(this)||this;return o._baseUrl="/Contadores",o._httpService=new s.HttpFetchService,o._modalBase=t,o.InicializarModal(),o.RegistrarFormatos(),o}return n(t,e),t.prototype.NuevoContador=function(){var e=this;this._httpService.Post(this._baseUrl+"/NuevoContador",null,!1).then((function(t){e._modalBase.innerHTML=t,e.InicilizarFormContador(!0),e.MaskManager.ApplyMasks()})),this._modalInstance.open()},t.prototype.GuardarContador=function(){var e,t=this,o=null===(e=this._formData.get("RequestVerificationToken"))||void 0===e?void 0:e.toString();this._httpService.PostForm(this._baseUrl+"/CrearContador",this._formData,o).then((function(e){e.Result&&(t.LimpiarFormulario(),t.onContadorCreado&&(t.onContadorCreado(e.Payload),t.MaskManager.ApplyMasks())),M.toast({html:e.Message,classes:e.Result?"succes":"error"})})).catch((function(e){return console.log(e)}))},t.prototype.InicializarModal=function(){this._modalInstance=M.Modal.init(this._modalBase,{dismissible:!1,opacity:.5,inDuration:300,outDuration:200,startingTop:"6%",endingTop:"8%"})},t.prototype.InicilizarFormContador=function(e){var t=this;a(".select2").select2({dropdownAutoWidth:!0,width:"100%"}),M.updateTextFields(),this._formContador=document.querySelector("#contador-form"),this._formData=new FormData(this._formContador),a(this._formContador).removeData("validator").removeData("unobtrusiveValidation"),a.validator.unobtrusive.parse(this._formContador),document.getElementById("contador-modal-cancel").addEventListener("click",(function(e){return t.CerrarModal()})),this._formContador.onsubmit=function(o){return o.preventDefault(),a(t._formContador).valid()&&(t._formData=new FormData(t._formContador),e&&t.GuardarContador()),!1}},t.prototype.RegistrarFormatos=function(){this.RegisterMasks([i.MaskFormats.Alfanumerico("#contador-id")])},t.prototype.LimpiarFormulario=function(){this._formContador.reset(),a("input").not("input[type= 'hidden']").val(null),a("#select2").val(null).trigger("change"),M.updateTextFields(),a("ul#contador-terminals").bonsai("update")},t.prototype.CerrarModal=function(){this._modalInstance.close(),this._formContador.parentNode.removeChild(this._formContador),this._contadorIdActualizar=""},t}(o("./Scripts/Core/Page.ts").Page);t.ContadoresGestionPage=d},"./Scripts/Shared/Components/ConfigureDataTable.ts":(e,t,o)=>{var r=o("./node_modules/jquery/dist/jquery.js");Object.defineProperty(t,"__esModule",{value:!0}),t.ConfigureDataTable=void 0,o("./node_modules/datatables.net-responsive/js/dataTables.responsive.js");var a=function(){function e(){}return e.prototype.Configure=function(e,t){var o=r(e).DataTable({columnDefs:t,destroy:!0,responsive:!0,language:{processing:"Procesando...",lengthMenu:"Mostrar _MENU_ registros",zeroRecords:"No se encontraron resultados",emptyTable:"Ningún dato disponible en esta tabla",info:"Mostrando del _START_ al _END_ de _TOTAL_ registros",infoEmpty:"Mostrando del 0 al 0 de 0 registros",infoFiltered:"(filtrado de un total de _MAX_ registros)",search:"Buscar:",thousands:",",loadingRecords:"Cargando...",paginate:{first:"Primero",last:"Último",next:"Siguiente",previous:"Anterior"},aria:{sortAscending:": Activar para ordenar la columna de manera ascendente",sortDescending:": Activar para ordenar la columna de manera descendente"}}});return this.FilterGlobal(o),o},e.prototype.ConfigureScrollX=function(e,t,o,a){var n=r(e).DataTable({columnDefs:o,columns:t,destroy:!0,scrollX:!0,language:{processing:"Procesando...",lengthMenu:"Mostrar _MENU_ registros",zeroRecords:"No se encontraron resultados",emptyTable:"Ningún dato disponible en esta tabla",info:"Mostrando del _START_ al _END_ de _TOTAL_ registros",infoEmpty:"Mostrando del 0 al 0 de 0 registros",infoFiltered:"(filtrado de un total de _MAX_ registros)",search:"Buscar:",thousands:",",loadingRecords:"Cargando...",paginate:{first:"Primero",last:"Último",next:"Siguiente",previous:"Anterior"},aria:{sortAscending:": Activar para ordenar la columna de manera ascendente",sortDescending:": Activar para ordenar la columna de manera descendente"}}});return this.FilterGlobal(n,a),n},e.prototype.ConfigureScrollXSinInfo=function(e,t){return r(e).DataTable({columnDefs:t,destroy:!0,scrollX:!0,paging:!1,ordering:!1,searching:!1,info:!1,language:{processing:"Procesando...",lengthMenu:"Mostrar _MENU_ registros",zeroRecords:"No se encontraron resultados",emptyTable:"Ningún dato disponible en esta tabla",info:"Mostrando del _START_ al _END_ de _TOTAL_ registros",infoEmpty:"Mostrando del 0 al 0 de 0 registros",infoFiltered:"(filtrado de un total de _MAX_ registros)",search:"Buscar:",thousands:",",loadingRecords:"Cargando...",paginate:{first:"Primero",last:"Último",next:"Siguiente",previous:"Anterior"},aria:{sortAscending:": Activar para ordenar la columna de manera ascendente",sortDescending:": Activar para ordenar la columna de manera descendente"}}})},e.prototype.FilterGlobal=function(e,t){t?r("input#"+t).on("keyup click",(function(){e.search(r("#"+t).val().toString(),r("#global_regex").prop("checked"),r("#global_smart").prop("checked")).draw()})):r("input#global_filter").on("keyup click",(function(){e.search(r("#global_filter").val().toString(),r("#global_regex").prop("checked"),r("#global_smart").prop("checked")).draw()})),r("input.column_filter").on("keyup click",(function(){var t=r(this).parents("tr").attr("data-column");e.column(t).search(r("#col"+t+"_filter").val().toString(),r("#col"+t+"_regex").prop("checked"),r("#col"+t+"_smart").prop("checked")).draw()}))},e}();t.ConfigureDataTable=a},"./Scripts/Shared/Components/ConfirmModalMessage.ts":function(e,t){var o=this&&this.__awaiter||function(e,t,o,r){return new(o||(o=Promise))((function(a,n){function s(e){try{d(r.next(e))}catch(e){n(e)}}function i(e){try{d(r.throw(e))}catch(e){n(e)}}function d(e){var t;e.done?a(e.value):(t=e.value,t instanceof o?t:new o((function(e){e(t)}))).then(s,i)}d((r=r.apply(e,t||[])).next())}))},r=this&&this.__generator||function(e,t){var o,r,a,n,s={label:0,sent:function(){if(1&a[0])throw a[1];return a[1]},trys:[],ops:[]};return n={next:i(0),throw:i(1),return:i(2)},"function"==typeof Symbol&&(n[Symbol.iterator]=function(){return this}),n;function i(n){return function(i){return function(n){if(o)throw new TypeError("Generator is already executing.");for(;s;)try{if(o=1,r&&(a=2&n[0]?r.return:n[0]?r.throw||((a=r.return)&&a.call(r),0):r.next)&&!(a=a.call(r,n[1])).done)return a;switch(r=0,a&&(n=[2&n[0],a.value]),n[0]){case 0:case 1:a=n;break;case 4:return s.label++,{value:n[1],done:!1};case 5:s.label++,r=n[1],n=[0];continue;case 7:n=s.ops.pop(),s.trys.pop();continue;default:if(!((a=(a=s.trys).length>0&&a[a.length-1])||6!==n[0]&&2!==n[0])){s=0;continue}if(3===n[0]&&(!a||n[1]>a[0]&&n[1]<a[3])){s.label=n[1];break}if(6===n[0]&&s.label<a[1]){s.label=a[1],a=n;break}if(a&&s.label<a[2]){s.label=a[2],s.ops.push(n);break}a[2]&&s.ops.pop(),s.trys.pop();continue}n=t.call(e,s)}catch(e){n=[6,e],r=0}finally{o=a=0}if(5&n[0])throw n[1];return{value:n[0]?n[1]:void 0,done:!0}}([n,i])}}};Object.defineProperty(t,"__esModule",{value:!0}),t.ConfirmModalMessage=void 0;var a=function(){function e(e,t,o,r){this.title=e,this.message=t,this.acceptText=o,this.dismissText=r,this.idMesssage="confirm-message",this.parent=document.body,this.CreateModal(),this.AppendModel()}return e.prototype.CreateModal=function(){this.modal=document.createElement("div"),this.modal.id=this.idMesssage,this.modal.classList.add("modal","modal-fixed-footer","modal-confirm-message-small");var e=document.createElement("div");e.classList.add("modal-content"),this.modal.appendChild(e);var t=document.createElement("h5");t.textContent=this.title,e.appendChild(t);var o=document.createElement("p");o.textContent=this.message,e.appendChild(o);var r=document.createElement("div");r.classList.add("modal-footer"),this.modal.appendChild(r),this.dismissButton=document.createElement("button"),this.dismissButton.classList.add("modal-action","waves-effect","waves-red","btn-flat","btn-orange"),this.dismissButton.type="button",this.dismissButton.textContent=this.dismissText,r.appendChild(this.dismissButton),this.acceptButton=document.createElement("button"),this.acceptButton.classList.add("modal-action","waves-effect","waves-red","btn-flat","btn-orange"),this.acceptButton.type="button",this.acceptButton.textContent=this.acceptText,r.appendChild(this.acceptButton)},e.prototype.AppendModel=function(){this.parent.appendChild(this.modal)},e.prototype.destroy=function(){this.modalInstance&&(this.modalInstance.close(),this.modalInstance.destroy());var e=document.querySelector("#"+this.idMesssage);e.parentNode.removeChild(e)},e.prototype.Confirm=function(){return o(this,void 0,void 0,(function(){var e=this;return r(this,(function(t){return[2,new Promise((function(t,o){(!e.modal||!e.acceptButton||!e.dismissButton)&&(e.destroy(),o("Fallo algo en la creación del mensaje")),M.Modal.init(e.modal,{dismissible:!1,opacity:.5,inDuration:300,outDuration:200,startingTop:"6%",endingTop:"8%"}),e.modalInstance=M.Modal.getInstance(e.modal),e.modalInstance.open(),e.acceptButton.addEventListener("click",(function(){t(!0),e.destroy()})),e.dismissButton.addEventListener("click",(function(){t(!1),e.destroy()}))}))]}))}))},e}();t.ConfirmModalMessage=a},"./Scripts/Shared/Http/HttpFetchService.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.HttpFetchService=void 0;var o=function(){function e(){}return e.prototype.Post=function(e,t,o){return void 0===o&&(o=!0),new Promise((function(r,a){fetch(e,{method:"post",headers:{"Content-Type":"application/json"},body:JSON.stringify(t)}).then((function(e){return o?e.json():e.text()})).then((function(e){r(e)})).catch((function(e){console.log(e),a(e)}))}))},e.prototype.PostForm=function(e,t,o,r){return void 0===r&&(r=!0),new Promise((function(a,n){fetch(e,{method:"post",headers:{RequestVerificationToken:o},body:t}).then((function(e){return r?e.json():e.text()})).then((function(e){a(e)})).catch((function(e){console.log(e),n(e)}))}))},e.prototype.PostFormURL=function(e,t,o){return void 0===o&&(o=!0),new Promise((function(r,a){fetch(e,{method:"post",headers:{"Content-Type":"application/json"},body:t}).then((function(e){return o?e.json():e.text()})).then((function(e){r(e)})).catch((function(e){console.log(e),a(e)}))}))},e}();t.HttpFetchService=o},"./Scripts/Shared/Http/index.ts":function(e,t,o){var r=this&&this.__createBinding||(Object.create?function(e,t,o,r){void 0===r&&(r=o),Object.defineProperty(e,r,{enumerable:!0,get:function(){return t[o]}})}:function(e,t,o,r){void 0===r&&(r=o),e[r]=t[o]}),a=this&&this.__exportStar||function(e,t){for(var o in e)"default"===o||Object.prototype.hasOwnProperty.call(t,o)||r(t,e,o)};Object.defineProperty(t,"__esModule",{value:!0}),t.services=void 0;var n=o("./Scripts/Shared/Http/HttpFetchService.ts");t.services=[n.HttpFetchService],a(o("./Scripts/Shared/Http/HttpFetchService.ts"),t)},"./Scripts/Shared/Models/ActionsPermission.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.ActionsPermission=void 0;t.ActionsPermission=function(){}},"./Scripts/Shared/Models/Area.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Area=void 0;t.Area=function(){}},"./Scripts/Shared/Models/Cabezote.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Cabezote=void 0;t.Cabezote=function(){}},"./Scripts/Shared/Models/Compañia.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t["Compañia"]=void 0;t["Compañia"]=function(){}},"./Scripts/Shared/Models/Conductor.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Conductor=void 0;t.Conductor=function(){}},"./Scripts/Shared/Models/Contador.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Contador=void 0;t.Contador=function(){}},"./Scripts/Shared/Models/Despacho.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Despacho=void 0;t.Despacho=function(){}},"./Scripts/Shared/Models/FechasCorteDTO.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.FechasCorteDTO=void 0;t.FechasCorteDTO=function(){}},"./Scripts/Shared/Models/Linea.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Linea=void 0;t.Linea=function(){}},"./Scripts/Shared/Models/Log.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Log=void 0;t.Log=function(){}},"./Scripts/Shared/Models/MessageResponse.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0})},"./Scripts/Shared/Models/ProcesamientoArchivos.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Paso2Mapeo=t.Paso1Mapeo=t.MapeoArchivos=void 0;t.MapeoArchivos=function(){};t.Paso1Mapeo=function(){};t.Paso2Mapeo=function(){}},"./Scripts/Shared/Models/Producto.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Producto=void 0;t.Producto=function(){}},"./Scripts/Shared/Models/Proveedor.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Proveedor=void 0;t.Proveedor=function(){}},"./Scripts/Shared/Models/Rol.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Rol=void 0;t.Rol=function(){}},"./Scripts/Shared/Models/Tanque.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Tanque=void 0;t.Tanque=function(){}},"./Scripts/Shared/Models/Terminal.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Terminal=void 0;t.Terminal=function(){}},"./Scripts/Shared/Models/Trailer.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Trailer=void 0;t.Trailer=function(){}},"./Scripts/Shared/Models/Usuario.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Usuario=void 0;t.Usuario=function(){}},"./Scripts/Shared/Models/index.ts":function(e,t,o){var r=this&&this.__createBinding||(Object.create?function(e,t,o,r){void 0===r&&(r=o),Object.defineProperty(e,r,{enumerable:!0,get:function(){return t[o]}})}:function(e,t,o,r){void 0===r&&(r=o),e[r]=t[o]}),a=this&&this.__exportStar||function(e,t){for(var o in e)"default"===o||Object.prototype.hasOwnProperty.call(t,o)||r(t,e,o)};Object.defineProperty(t,"__esModule",{value:!0}),t.models=void 0;var n=o("./Scripts/Shared/Models/Usuario.ts"),s=o("./Scripts/Shared/Models/Cabezote.ts"),i=o("./Scripts/Shared/Models/Trailer.ts"),d=o("./Scripts/Shared/Models/Conductor.ts"),l=o("./Scripts/Shared/Models/Area.ts"),c=o("./Scripts/Shared/Models/Compañia.ts"),u=o("./Scripts/Shared/Models/Log.ts"),p=o("./Scripts/Shared/Models/Linea.ts"),f=o("./Scripts/Shared/Models/Rol.ts"),h=o("./Scripts/Shared/Models/Terminal.ts"),m=o("./Scripts/Shared/Models/Tanque.ts"),v=o("./Scripts/Shared/Models/Contador.ts"),M=o("./Scripts/Shared/Models/Proveedor.ts"),S=o("./Scripts/Shared/Models/ProcesamientoArchivos.ts"),y=o("./Scripts/Shared/Models/Producto.ts"),g=o("./Scripts/Shared/Models/Despacho.ts"),b=o("./Scripts/Shared/Models/FechasCorteDTO.ts");t.models=[n.Usuario,s.Cabezote,i.Trailer,d.Conductor,l.Area,u.Log,f.Rol,c.Compañia,h.Terminal,m.Tanque,v.Contador,p.Linea,M.Proveedor,S.MapeoArchivos,y.Producto,g.Despacho,b.FechasCorteDTO],a(o("./Scripts/Shared/Models/MessageResponse.ts"),t),a(o("./Scripts/Shared/Models/Usuario.ts"),t),a(o("./Scripts/Shared/Models/Cabezote.ts"),t),a(o("./Scripts/Shared/Models/Trailer.ts"),t),a(o("./Scripts/Shared/Models/Conductor.ts"),t),a(o("./Scripts/Shared/Models/Area.ts"),t),a(o("./Scripts/Shared/Models/Linea.ts"),t),a(o("./Scripts/Shared/Models/Compañia.ts"),t),a(o("./Scripts/Shared/Models/Rol.ts"),t),a(o("./Scripts/Shared/Models/Terminal.ts"),t),a(o("./Scripts/Shared/Models/Tanque.ts"),t),a(o("./Scripts/Shared/Models/Contador.ts"),t),a(o("./Scripts/Shared/Models/Log.ts"),t),a(o("./Scripts/Shared/Models/Proveedor.ts"),t),a(o("./Scripts/Shared/Models/ProcesamientoArchivos.ts"),t),a(o("./Scripts/Shared/Models/Producto.ts"),t),a(o("./Scripts/Shared/Models/Despacho.ts"),t),a(o("./Scripts/Shared/Models/FechasCorteDTO.ts"),t)},"./Scripts/Shared/Utils/HTMLExtensions.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),HTMLElement.prototype.on=function(e,t,o){this.addEventListener(e,(function(e){e.target.matches(t)&&o(e)}))}},"./Scripts/Shared/Utils/JQValidations.ts":(e,t,o)=>{var r=o("./node_modules/jquery/dist/jquery.js");Object.defineProperty(t,"__esModule",{value:!0}),t.JQValidations=void 0,o("./node_modules/jquery-validation/dist/jquery.validate.js"),o("./node_modules/jquery-validation-unobtrusive/dist/jquery.validate.unobtrusive.js");var a=function(){function e(){}return e.MaxFileSizeValidation=function(){r.validator.methods.maxfilesize||(r.validator.addMethod("maxfilesize",(function(e,t,o){var r=o,a=t.files[0];return!(a&&a.size/1024>r)})),r.validator.unobtrusive.adapters.add("maxfilesize",["size"],(function(e){e.rules.maxfilesize=e.params.size,e.messages.maxfilesize=e.message})))},e.AllowedExtensionsValidation=function(){r.validator.methods.allowedextensions||(r.validator.addMethod("allowedextensions",(function(e,t,o){var r=o,a=t.files[0],n=r.split(",");return!a||!!new RegExp("("+n.join("|").replace(/\./g,"\\.")+")$").test(e)})),r.validator.unobtrusive.adapters.add("allowedextensions",["exts"],(function(e){e.rules.allowedextensions=e.params.exts,e.messages.allowedextensions=e.message})))},e.NotEqualValidation=function(){r.validator.methods.notequal||(r.validator.addMethod("notequal",(function(e,t,o){var r=o,a=t.value,n=document.getElementById(r).value;return!a||a!=n})),r.validator.unobtrusive.adapters.add("notequal",["IdinputTocompare"],(function(e){e.rules.notequal=e.params.IdinputTocompare,e.messages.notequal=e.message})))},e}();t.JQValidations=a},"./Scripts/Shared/Utils/MaskFormats.ts":(e,t,o)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.MaskFormats=void 0;var r=o("./node_modules/imask/esm/index.js"),a=function(){function e(){}return e.IntegerFormat=function(){return{Selector:".formato-int",Mask:{mask:Number,scale:0,signed:!1,thousandsSeparator:",",padFractionalZeros:!1,normalizeZeros:!0,radix:".",mapToRadix:["."]}}},e.DecimalFormat=function(){return{Selector:".formato-decimal",Mask:{mask:Number,scale:2,signed:!1,thousandsSeparator:",",padFractionalZeros:!1,normalizeZeros:!0,radix:".",mapToRadix:["."]}}},e.DateShortFormat=function(){return{Selector:".formato-fecha-corta",Mask:{mask:"00{/}MMM{/}0000",overwrite:!0,autofix:!0,blocks:{MMM:{mask:r.default.MaskedEnum,enum:["ene","feb","mar","abr","may","jun","jul","ago","sep","oct","nov","dic"]}}}}},e.DateLongFormat=function(){return{Selector:".formato-fecha-larga",Mask:{mask:"00{/}MMM{/}0000{ }HH{:}TT{:}TT",overwrite:!0,autofix:!0,blocks:{MMM:{mask:r.default.MaskedEnum,enum:["ene","feb","mar","abr","may","jun","jul","ago","sep","oct","nov","dic"]},HH:{mask:r.default.MaskedRange,from:0,to:24},TT:{mask:r.default.MaskedRange,from:0,to:59}}}}},e.IntegerFormatRang=function(e,t,o){return{Selector:e,Mask:{mask:Number,min:t,max:o,scale:0,signed:!1,thousandsSeparator:",",padFractionalZeros:!1,normalizeZeros:!0,radix:".",mapToRadix:["."]}}},e.DecimalFormatRang=function(e,t,o){return{Selector:e,Mask:{mask:Number,min:t,max:o,scale:2,signed:!1,thousandsSeparator:",",padFractionalZeros:!1,normalizeZeros:!0,radix:".",mapToRadix:["."]}}},e.Alfanumerico=function(e){return{Selector:e,Mask:{mask:/^[a-z0-9]+$/i,lazy:!1}}},e.NumericoSinSignos=function(e){return{Selector:e,Mask:{mask:/^[0-9]+$/i,lazy:!1}}},e}();t.MaskFormats=a},"./Scripts/Shared/Utils/MaskFormatsManager.ts":(e,t,o)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.MaskFormatsManager=void 0;var r=o("./node_modules/imask/esm/index.js"),a=function(){function e(){this.FormatElements=new Array}return e.prototype.RegisterMasks=function(e){this.CleanAll(),this.Masks=e},e.prototype.ApplyMasks=function(){var e,t=this;this.CleanFormats(),null===(e=this.Masks)||void 0===e||e.forEach((function(e,o,r){t.ApplyFormat(e)}))},e.prototype.AddApplyMask=function(e){this.Masks.push(e),this.ApplyFormat(e)},e.prototype.UnmaskFormats=function(){var e;null===(e=this.FormatElements)||void 0===e||e.forEach((function(e,t,o){e.MaskInput.el.input.value=e.MaskInput.unmaskedValue,e.MaskInput.masked.reset()}))},e.prototype.SetUnmaskedFormValue=function(e){var t;null===(t=this.FormatElements)||void 0===t||t.forEach((function(t,o,r){e.set(t.MaskInput.el.input.name,t.MaskInput.unmaskedValue),t.MaskInput.masked.reset()}))},e.prototype.UpdateValue=function(e){var t=this;(null==e?void 0:e.length)>0&&e.forEach((function(e){var o=t.FormatElements.filter((function(t){return t.Selector===e}));(null==o?void 0:o.length)>0&&o.forEach((function(e){e.MaskInput.updateValue(),e.MaskInput.unmaskedValue=e.MaskInput.el.value}))}))},e.prototype.ApplyFormat=function(e){var t=this;document.querySelectorAll(e.Selector).forEach((function(o){t.FormatElements.push({Selector:e.Selector,MaskInput:r.default(o,e.Mask)})}))},e.prototype.CleanAll=function(){var e;(null===(e=this.Masks)||void 0===e?void 0:e.length)>0&&this.Masks.forEach((function(e,t,o){o.splice(t,1)})),this.CleanFormats()},e.prototype.CleanFormats=function(){var e;(null===(e=this.FormatElements)||void 0===e?void 0:e.length)>0&&this.FormatElements.forEach((function(e,t,o){e.MaskInput.masked.reset(),e.MaskInput.destroy,o.splice(t,1)}))},e.prototype.Destroy=function(){this.CleanAll(),this.Masks=null,this.FormatElements=null},e}();t.MaskFormatsManager=a},"./Scripts/Shared/Utils/SelectorMenu.ts":(e,t,o)=>{var r=o("./node_modules/jquery/dist/jquery.js");Object.defineProperty(t,"__esModule",{value:!0}),t.SelectorMenu=void 0;var a=function(){function e(){}return e.SeleccionEnElMenu=function(){var e=location.href,t=e.indexOf("/",8),o="a[href$='"+e.substr(t,e.length)+"']";r(".nav-list li a").removeClass("active"),r(".collapsible-body").css({display:""}),r(".listaPadre").removeClass("active"),r(o).addClass("active");var a=r(o).attr("name");r("#"+a).addClass("active"),r("#"+a+" .collapsible-body").css({display:"block"});var n=r("#"+a).attr("name");r("#"+n).addClass("active"),r("#"+n+" #"+a+" .collapsible-body").css({display:"block"}),r("#"+n+" .nivel2").css({display:"block"}),r("#"+n+" .collapsible-body").first().css({display:"block"})},e}();t.SelectorMenu=a},"./Scripts/Shared/Utils/index.ts":function(e,t,o){var r=this&&this.__createBinding||(Object.create?function(e,t,o,r){void 0===r&&(r=o),Object.defineProperty(e,r,{enumerable:!0,get:function(){return t[o]}})}:function(e,t,o,r){void 0===r&&(r=o),e[r]=t[o]}),a=this&&this.__exportStar||function(e,t){for(var o in e)"default"===o||Object.prototype.hasOwnProperty.call(t,o)||r(t,e,o)};Object.defineProperty(t,"__esModule",{value:!0}),t.components=void 0;var n=o("./Scripts/Shared/Utils/JQValidations.ts"),s=o("./Scripts/Shared/Utils/MaskFormats.ts"),i=o("./Scripts/Shared/Utils/MaskFormatsManager.ts");t.components=[n.JQValidations,s.MaskFormats,i.MaskFormatsManager],a(o("./Scripts/Shared/Utils/JQValidations.ts"),t),a(o("./Scripts/Shared/Utils/MaskFormats.ts"),t),a(o("./Scripts/Shared/Utils/MaskFormatsManager.ts"),t)},"./Scripts/Shared/index.ts":function(e,t,o){var r=this&&this.__createBinding||(Object.create?function(e,t,o,r){void 0===r&&(r=o),Object.defineProperty(e,r,{enumerable:!0,get:function(){return t[o]}})}:function(e,t,o,r){void 0===r&&(r=o),e[r]=t[o]}),a=this&&this.__exportStar||function(e,t){for(var o in e)"default"===o||Object.prototype.hasOwnProperty.call(t,o)||r(t,e,o)};Object.defineProperty(t,"__esModule",{value:!0}),a(o("./Scripts/Shared/Http/index.ts"),t),a(o("./Scripts/Shared/Models/index.ts"),t),a(o("./Scripts/Shared/Utils/HTMLExtensions.ts"),t)}},t={};function o(r){if(t[r])return t[r].exports;var a=t[r]={exports:{}};return e[r].call(a.exports,a,a.exports,o),a.exports}return o.m=e,o.x=e=>{},o.d=(e,t)=>{for(var r in t)o.o(t,r)&&!o.o(e,r)&&Object.defineProperty(e,r,{enumerable:!0,get:t[r]})},o.o=(e,t)=>Object.prototype.hasOwnProperty.call(e,t),o.r=e=>{"undefined"!=typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(e,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(e,"__esModule",{value:!0})},(()=>{var e={contadores:0},t=[["./Scripts/Pages/Contadores/Contadores.ts","packages"]],r=e=>{},a=(a,n)=>{for(var s,i,[d,l,c,u]=n,p=0,f=[];p<d.length;p++)i=d[p],o.o(e,i)&&e[i]&&f.push(e[i][0]),e[i]=0;for(s in l)o.o(l,s)&&(o.m[s]=l[s]);for(c&&c(o),a&&a(n);f.length;)f.shift()();return u&&t.push.apply(t,u),r()},n=self.webpackChunkkairosv2=self.webpackChunkkairosv2||[];function s(){for(var r,a=0;a<t.length;a++){for(var n=t[a],s=!0,i=1;i<n.length;i++){var d=n[i];0!==e[d]&&(s=!1)}s&&(t.splice(a--,1),r=o(o.s=n[0]))}return 0===t.length&&(o.x(),o.x=e=>{}),r}n.forEach(a.bind(null,0)),n.push=a.bind(null,n.push.bind(n));var i=o.x;o.x=()=>(o.x=i||(e=>{}),(r=s)())})(),o.x()})()}));
//# sourceMappingURL=contadores.js.map