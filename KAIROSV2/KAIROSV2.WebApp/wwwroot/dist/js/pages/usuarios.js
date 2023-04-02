/*! For license information please see usuarios.js.LICENSE.txt */
!function(e,t){"object"==typeof exports&&"object"==typeof module?module.exports=t():"function"==typeof define&&define.amd?define([],t):"object"==typeof exports?exports.kairosv2=t():e.kairosv2=t()}(self,(function(){return(()=>{"use strict";var e={"./Scripts/Core/Page.ts":(e,t,o)=>{var a=o("./node_modules/jquery/dist/jquery.js");Object.defineProperty(t,"__esModule",{value:!0}),t.Page=void 0;var r=o("./node_modules/dayjs/dayjs.min.js"),s=o("./Scripts/Shared/Utils/index.ts");o("./node_modules/dayjs/locale/es-do.js");var i=o("./Scripts/Shared/Models/ActionsPermission.ts"),n=o("./Scripts/Shared/index.ts"),l=o("./node_modules/dayjs/plugin/customParseFormat.js"),d=function(){function e(){r.locale("es-do"),r.extend(l),this.AdjustValidation()}return e.prototype.RegisterMasks=function(e){this.MaskManager||(this.MaskManager=new s.MaskFormatsManager),this.MaskManager.RegisterMasks(e)},e.prototype.GetPermissions=function(e){var t=this;(new n.HttpFetchService).Post(e,"",!0).then((function(e){e&&(t.Permisions=e)})).catch((function(e){t.Permisions=new i.ActionsPermission}))},e.prototype.Destroy=function(){var e;null===(e=this.MaskManager)||void 0===e||e.Destroy()},e.prototype.AdjustValidation=function(){a.validator.methods.range=function(e,t,o){var a=e.replace(",","");return this.optional(t)||a>=o[0]&&a<=o[1]}},e}();t.Page=d},"./Scripts/Pages/Usuarios/GestionUsuarios.ts":(e,t,o)=>{var a=o("./node_modules/jquery/dist/jquery.js");Object.defineProperty(t,"__esModule",{value:!0}),t.UsuariosGestionPage=void 0;var r=o("./Scripts/Shared/index.ts"),s=o("./Scripts/Shared/Models/Usuario.ts");o("./node_modules/jquery-qubit/jquery.qubit.js"),o("./node_modules/jquery-bonsai/jquery.bonsai.js"),o("./node_modules/jquery-bonsai/jquery.bonsai.css"),o("./node_modules/select2/dist/js/select2.js"),o("./node_modules/select2/dist/css/select2.css");var i=function(){function e(e){this._baseUrl="/Usuarios",this._httpService=new r.HttpFetchService,this._modalBase=e,this.InicializarModal()}return e.prototype.NuevoUsuario=function(){var e=this;this._httpService.Post(this._baseUrl+"/NuevoUsuario",null,!1).then((function(t){e._modalBase.innerHTML=t,e.InicilizarFormUsuario(!0)})),this._modalInstance.open()},e.prototype.GuardarUsuario=function(){var e,t=this,o=null===(e=this._formData.get("RequestVerificationToken"))||void 0===e?void 0:e.toString();this._httpService.PostForm(this._baseUrl+"/CrearUsuario",this._formData,o).then((function(e){e.Result&&(t.LimpiarFormulario(),t.onUsuarioCreado&&t.onUsuarioCreado(t.ExtraerUsuario())),M.toast({html:e.Message,classes:e.Result?"succes":"error"})})).catch((function(e){return console.log(e)}))},e.prototype.DatosUsuario=function(e,t){var o=this,a={idEntidad:e,lectura:t};this._usuarioIdActualizar=e,this._httpService.Post(this._baseUrl+"/DatosUsuario",a,!1).then((function(e){o._modalBase.innerHTML=e,o.InicilizarFormUsuario(!1)})),this._modalInstance.open()},e.prototype.ActualizarUsuario=function(){var e,t=this,o=null===(e=this._formData.get("RequestVerificationToken"))||void 0===e?void 0:e.toString();this._formData.set("IdUsuario",this._usuarioIdActualizar),this._httpService.PostForm(this._baseUrl+"/ActualizarUsuario",this._formData,o).then((function(e){e.Result&&(t.onUsuarioActualizado&&t.onUsuarioActualizado(t.ExtraerUsuario()),t.LimpiarFormulario(),t.CerrarModal()),M.toast({html:e.Message,classes:e.Result?"succes":"error"})})).catch((function(e){return console.log(e)}))},e.prototype.InicializarModal=function(){this._modalInstance=M.Modal.init(this._modalBase,{dismissible:!1,opacity:.5,inDuration:300,outDuration:200,startingTop:"6%",endingTop:"8%"})},e.prototype.InicilizarFormUsuario=function(e){var t=this;a(".select2").select2({dropdownAutoWidth:!0,width:"100%",language:"es"}),a(".select2").select2().change((function(){a(this).valid()})),this._selects=M.FormSelect.init(document.querySelectorAll("select")),this._collaps=M.Collapsible.init(document.querySelectorAll(".collapsible.user")),M.updateTextFields(),ImoglayInput(document.getElementById("user-img"),"camera_alt","Subir foto",!0),a("ul#user-terminals").bonsai({expandAll:!0,checkboxes:!0}),this._formUser=document.querySelector("#user-form"),this._formData=new FormData(this._formUser),a(this._formUser).removeData("validator").removeData("unobtrusiveValidation"),a.validator.unobtrusive.parse(this._formUser),this.ConfigurarBusquedaUsuario(),document.getElementById("user-modal-cancel").addEventListener("click",(function(e){return t.CerrarModal()})),this._formUser.onsubmit=function(o){return o.preventDefault(),a(t._formUser).valid()&&(t._formData=new FormData(t._formUser),e?t.GuardarUsuario():t.ActualizarUsuario()),!1},document.getElementById("user-collapsible").addEventListener("click",(function(e){return t.BajarScroll()}))},e.prototype.ExtraerUsuario=function(){var e,t,o,a,r,i=new s.Usuario;return i.Imagen=this._formData.get("Imagen"),i.Email=null===(e=this._formData.get("Email"))||void 0===e?void 0:e.toString(),i.Telefono=null===(t=this._formData.get("Telefono"))||void 0===t?void 0:t.toString(),i.IdUsuario=null===(o=this._formData.get("IdUsuario"))||void 0===o?void 0:o.toString(),i.Nombre=null===(a=this._formData.get("Nombre"))||void 0===a?void 0:a.toString(),i.Rol=null===(r=this._formData.get("RolId"))||void 0===r?void 0:r.toString(),i},e.prototype.LimpiarFormulario=function(){this._formUser.reset(),a("input").not("input[type= 'hidden']").val(null),a("select").val(null),a("#user-id").val(null).trigger("change");var e=document.getElementById("user-img");e.value=null;var t=new Event("change");e.dispatchEvent(t),M.updateTextFields(),a("ul#user-terminals").bonsai("update")},e.prototype.CerrarModal=function(){this._selects.every((function(e){return e.destroy()})),this._collaps.every((function(e){return e.destroy()})),this._modalInstance.close(),this._formUser.parentNode.removeChild(this._formUser),this._usuarioIdActualizar=""},e.prototype.BajarScroll=function(){a("#user-modalcontent").animate({scrollTop:a("#user-modalcontent").prop("scrollHeight")},1e3)},e.prototype.ConfigurarBusquedaUsuario=function(){var e=this;a("#user-id").on("select2:select",(function(t){var o;o=t.params.data,document.getElementById("user-name").value=o.text,document.getElementById("user-phone").value=o.telefono,document.getElementById("user-email").value=o.email,M.updateTextFields(),a(e._formUser).valid()})).select2({language:"es",dropdownAutoWidth:!0,width:"100%",ajax:{url:"/Usuarios/ObtenerUsuariosActiveDirectory",type:"Post",dataType:"json",delay:250,data:function(e){return{busqueda:e.term}},processResults:function(e){return e.results=e.map((function(e){return{text:e.Text,id:e.Id,email:e.Email,telefono:e.Telefono}})),{results:e.results}},cache:!0},placeholder:"Buscar usuario",escapeMarkup:function(e){return e},minimumInputLength:3,templateResult:this.FormatearSelect2,templateSelection:function(e){return e.id}})},e.prototype.FormatearSelect2=function(e){return e.id?"<div> <h6>"+e.id+'</h6><div class="light">'+e.text+"</div><div>"+e.email+"</div><div>"+e.telefono+"</div> </div>":'<div class="preloader-wrapper small active center-align center"><div class="spinner-layer spinner-green-only" ><div class="circle-clipper left" ><div class="circle" > </div></div><div class="gap-patch"><div class="circle" > </div></div><div class="circle-clipper right"><div class="circle" > </div> < /div> < /div> </div>'},e}();t.UsuariosGestionPage=i},"./Scripts/Pages/Usuarios/Usuarios.ts":function(e,t,o){var a,r=o("./node_modules/jquery/dist/jquery.js"),s=this&&this.__extends||(a=function(e,t){return(a=Object.setPrototypeOf||{__proto__:[]}instanceof Array&&function(e,t){e.__proto__=t}||function(e,t){for(var o in t)Object.prototype.hasOwnProperty.call(t,o)&&(e[o]=t[o])})(e,t)},function(e,t){function o(){this.constructor=e}a(e,t),e.prototype=null===t?Object.create(t):(o.prototype=t.prototype,new o)}),i=this&&this.__awaiter||function(e,t,o,a){return new(o||(o=Promise))((function(r,s){function i(e){try{l(a.next(e))}catch(e){s(e)}}function n(e){try{l(a.throw(e))}catch(e){s(e)}}function l(e){var t;e.done?r(e.value):(t=e.value,t instanceof o?t:new o((function(e){e(t)}))).then(i,n)}l((a=a.apply(e,t||[])).next())}))},n=this&&this.__generator||function(e,t){var o,a,r,s,i={label:0,sent:function(){if(1&r[0])throw r[1];return r[1]},trys:[],ops:[]};return s={next:n(0),throw:n(1),return:n(2)},"function"==typeof Symbol&&(s[Symbol.iterator]=function(){return this}),s;function n(s){return function(n){return function(s){if(o)throw new TypeError("Generator is already executing.");for(;i;)try{if(o=1,a&&(r=2&s[0]?a.return:s[0]?a.throw||((r=a.return)&&r.call(a),0):a.next)&&!(r=r.call(a,s[1])).done)return r;switch(a=0,r&&(s=[2&s[0],r.value]),s[0]){case 0:case 1:r=s;break;case 4:return i.label++,{value:s[1],done:!1};case 5:i.label++,a=s[1],s=[0];continue;case 7:s=i.ops.pop(),i.trys.pop();continue;default:if(!((r=(r=i.trys).length>0&&r[r.length-1])||6!==s[0]&&2!==s[0])){i=0;continue}if(3===s[0]&&(!r||s[1]>r[0]&&s[1]<r[3])){i.label=s[1];break}if(6===s[0]&&i.label<r[1]){i.label=r[1],r=s;break}if(r&&i.label<r[2]){i.label=r[2],i.ops.push(s);break}r[2]&&i.ops.pop(),i.trys.pop();continue}s=t.call(e,i)}catch(e){s=[6,e],a=0}finally{o=r=0}if(5&s[0])throw s[1];return{value:s[0]?s[1]:void 0,done:!0}}([s,n])}}};Object.defineProperty(t,"__esModule",{value:!0}),t.UsuariosPage=void 0;var l=o("./Scripts/Shared/index.ts"),d=o("./Scripts/Shared/Components/ConfirmModalMessage.ts"),c=o("./Scripts/Shared/Components/ConfigureDataTable.ts"),u=o("./Scripts/Pages/Usuarios/GestionUsuarios.ts"),p=o("./Scripts/Shared/Utils/JQValidations.ts"),m=o("./Scripts/Shared/Utils/SelectorMenu.ts"),h=function(e){function t(){var t=e.call(this)||this;return t.BaseUrl="/Usuarios/Index",t.Init(),t}return s(t,e),t.prototype.Destroy=function(){e.prototype.Destroy.call(this),this.Table.destroy(!1)},t.prototype.Init=function(){this.InicializarButtons(),this.InicializarControls(),e.prototype.GetPermissions.call(this,"/Usuarios/ObtenerPermisos")},t.prototype.InicializarControls=function(){var e=this;m.SelectorMenu.SeleccionEnElMenu(),this.Table=(new c.ConfigureDataTable).Configure("#data-table-usuario",[{width:"5%",targets:0}]),this.gestionUsuarios=new u.UsuariosGestionPage(document.getElementById("user-modal")),this.gestionUsuarios.onUsuarioCreado=function(t){return e.AgregarFilaDatatable(t)},this.gestionUsuarios.onUsuarioActualizado=function(t){return e.ActualizarFilaDatatable(t)},p.JQValidations.MaxFileSizeValidation(),p.JQValidations.AllowedExtensionsValidation()},t.prototype.InicializarButtons=function(){var e=this,t=document.getElementById("data-table-usuario"),o=document.getElementById("user-add");t.on("click",".user-action",(function(t){t.target.matches(".user-edit")?e.EditarUsuario(t.target):t.target.matches(".user-delete")?e.BorrarUsuario(t.target):t.target.matches(".user-detail")&&e.DetallesUsuario(t.target)})),o.addEventListener("click",(function(t){return e.CrearUsuario()}))},t.prototype.BorrarUsuario=function(e){return i(this,void 0,void 0,(function(){var t,o,a=this;return n(this,(function(r){switch(r.label){case 0:return t=e.dataset.userid,o=e.dataset.username,[4,new d.ConfirmModalMessage("Eliminar Usuario","¿Desea eliminar el Usuario "+o+"?","Aceptar","Cancelar").Confirm()];case 1:return r.sent()&&(new l.HttpFetchService).Post("/Usuarios/BorrarUsuario",t).then((function(t){t.Result&&a.BorrarFilaDatatable(e),M.toast({html:t.Message,classes:t.Result?"succes":"error"})})).catch((function(e){M.toast({html:e,classes:"error"})})),[2]}}))}))},t.prototype.CrearUsuario=function(){this.gestionUsuarios.NuevoUsuario()},t.prototype.EditarUsuario=function(e){console.log(e),this.gestionUsuarios.DatosUsuario(e.dataset.userid,!1)},t.prototype.DetallesUsuario=function(e){this.gestionUsuarios.DatosUsuario(e.dataset.userid,!0)},t.prototype.BorrarFilaDatatable=function(e){var t=r(e).closest("tr");if(t.prev().hasClass("parent")){var o=this.Table.row(t.prev());o.remove(),o.draw()}var a=this.Table.row(t);a.remove(),a.draw()},t.prototype.AgregarFilaDatatable=function(e){var t=e.Imagen.name;console.log(t),""!=e.Imagen.name?this.Table.row.add(['<span class="avatar-contact avatar-online circle"><img src="'+URL.createObjectURL(e.Imagen)+'" alt="avatar"></span>',e.IdUsuario,e.Nombre,e.Rol,e.Email,e.Telefono,'<a href="#" class="'+this.DisableAction(this.Permisions.Editar)+'" data-turbolinks="false"><i class="material-icons user-action user-edit" data-userid="'+e.IdUsuario+'" data-username="'+e.Nombre+'">edit</i></a> <a href="#" class="'+this.DisableAction(this.Permisions.Borrar)+'" data-turbolinks="false"><i class="material-icons user-action user-delete" data-userid="'+e.IdUsuario+'" data-username="'+e.Nombre+'">delete</i></a> <a href="#" class="'+this.DisableAction(this.Permisions.Detalles)+'" data-turbolinks="false"><i class="material-icons user-action user-detail" data-userid="'+e.IdUsuario+'" data-username="'+e.Nombre+'">remove_red_eye</i></a>']).draw(!1):this.Table.row.add(['<span class="avatar-contact avatar-online circle"><img src="/images/avatar/account_circle-black-48dp.svg" alt="avatar"></span>',e.IdUsuario,e.Nombre,e.Rol,e.Email,e.Telefono,'<a href="#" class="'+this.DisableAction(this.Permisions.Editar)+'" data-turbolinks="false"><i class="material-icons user-action user-edit" data-userid="'+e.IdUsuario+'" data-username="'+e.Nombre+'">edit</i></a> <a href="#" class="'+this.DisableAction(this.Permisions.Borrar)+'" data-turbolinks="false"><i class="material-icons user-action user-delete" data-userid="'+e.IdUsuario+'" data-username="'+e.Nombre+'">delete</i></a> <a href="#" class="'+this.DisableAction(this.Permisions.Detalles)+'" data-turbolinks="false"><i class="material-icons user-action user-detail" data-userid="'+e.IdUsuario+'" data-username="'+e.Nombre+'">remove_red_eye</i></a>']).draw(!1)},t.prototype.ActualizarFilaDatatable=function(e){var t=r("tr#"+e.IdUsuario),o=this.Table.row(t).data();""!=e.Imagen.name?o[0]='<span class="avatar-contact avatar-online"><img src="'+URL.createObjectURL(e.Imagen)+'" alt="avatar"></span>':o[0]='<span class="avatar-contact avatar-online"><img src="/images/avatar/account_circle-black-48dp.svg" alt="avatar"></span>',o[3]=e.Rol,this.Table.row(t).data(o)},t.prototype.DisableAction=function(e){return e?"":"disable-action"},t}(o("./Scripts/Core/Page.ts").Page);t.UsuariosPage=h;var f=new h;document.addEventListener("turbolinks:render",(function(e){-1!=document.URL.indexOf("/Usuarios/Index")?f?f.Init():f=new h:null==f||f.Destroy()}))},"./Scripts/Shared/Components/ConfigureDataTable.ts":(e,t,o)=>{var a=o("./node_modules/jquery/dist/jquery.js");Object.defineProperty(t,"__esModule",{value:!0}),t.ConfigureDataTable=void 0,o("./node_modules/datatables.net-responsive/js/dataTables.responsive.js");var r=function(){function e(){}return e.prototype.Configure=function(e,t){var o=a(e).DataTable({columnDefs:t,destroy:!0,responsive:!0,language:{processing:"Procesando...",lengthMenu:"Mostrar _MENU_ registros",zeroRecords:"No se encontraron resultados",emptyTable:"Ningún dato disponible en esta tabla",info:"Mostrando del _START_ al _END_ de _TOTAL_ registros",infoEmpty:"Mostrando del 0 al 0 de 0 registros",infoFiltered:"(filtrado de un total de _MAX_ registros)",search:"Buscar:",thousands:",",loadingRecords:"Cargando...",paginate:{first:"Primero",last:"Último",next:"Siguiente",previous:"Anterior"},aria:{sortAscending:": Activar para ordenar la columna de manera ascendente",sortDescending:": Activar para ordenar la columna de manera descendente"}}});return this.FilterGlobal(o),o},e.prototype.ConfigureScrollX=function(e,t,o,r){var s=a(e).DataTable({columnDefs:o,columns:t,destroy:!0,scrollX:!0,language:{processing:"Procesando...",lengthMenu:"Mostrar _MENU_ registros",zeroRecords:"No se encontraron resultados",emptyTable:"Ningún dato disponible en esta tabla",info:"Mostrando del _START_ al _END_ de _TOTAL_ registros",infoEmpty:"Mostrando del 0 al 0 de 0 registros",infoFiltered:"(filtrado de un total de _MAX_ registros)",search:"Buscar:",thousands:",",loadingRecords:"Cargando...",paginate:{first:"Primero",last:"Último",next:"Siguiente",previous:"Anterior"},aria:{sortAscending:": Activar para ordenar la columna de manera ascendente",sortDescending:": Activar para ordenar la columna de manera descendente"}}});return this.FilterGlobal(s,r),s},e.prototype.ConfigureScrollXSinInfo=function(e,t){return a(e).DataTable({columnDefs:t,destroy:!0,scrollX:!0,paging:!1,ordering:!1,searching:!1,info:!1,language:{processing:"Procesando...",lengthMenu:"Mostrar _MENU_ registros",zeroRecords:"No se encontraron resultados",emptyTable:"Ningún dato disponible en esta tabla",info:"Mostrando del _START_ al _END_ de _TOTAL_ registros",infoEmpty:"Mostrando del 0 al 0 de 0 registros",infoFiltered:"(filtrado de un total de _MAX_ registros)",search:"Buscar:",thousands:",",loadingRecords:"Cargando...",paginate:{first:"Primero",last:"Último",next:"Siguiente",previous:"Anterior"},aria:{sortAscending:": Activar para ordenar la columna de manera ascendente",sortDescending:": Activar para ordenar la columna de manera descendente"}}})},e.prototype.FilterGlobal=function(e,t){t?a("input#"+t).on("keyup click",(function(){e.search(a("#"+t).val().toString(),a("#global_regex").prop("checked"),a("#global_smart").prop("checked")).draw()})):a("input#global_filter").on("keyup click",(function(){e.search(a("#global_filter").val().toString(),a("#global_regex").prop("checked"),a("#global_smart").prop("checked")).draw()})),a("input.column_filter").on("keyup click",(function(){var t=a(this).parents("tr").attr("data-column");e.column(t).search(a("#col"+t+"_filter").val().toString(),a("#col"+t+"_regex").prop("checked"),a("#col"+t+"_smart").prop("checked")).draw()}))},e}();t.ConfigureDataTable=r},"./Scripts/Shared/Components/ConfirmModalMessage.ts":function(e,t){var o=this&&this.__awaiter||function(e,t,o,a){return new(o||(o=Promise))((function(r,s){function i(e){try{l(a.next(e))}catch(e){s(e)}}function n(e){try{l(a.throw(e))}catch(e){s(e)}}function l(e){var t;e.done?r(e.value):(t=e.value,t instanceof o?t:new o((function(e){e(t)}))).then(i,n)}l((a=a.apply(e,t||[])).next())}))},a=this&&this.__generator||function(e,t){var o,a,r,s,i={label:0,sent:function(){if(1&r[0])throw r[1];return r[1]},trys:[],ops:[]};return s={next:n(0),throw:n(1),return:n(2)},"function"==typeof Symbol&&(s[Symbol.iterator]=function(){return this}),s;function n(s){return function(n){return function(s){if(o)throw new TypeError("Generator is already executing.");for(;i;)try{if(o=1,a&&(r=2&s[0]?a.return:s[0]?a.throw||((r=a.return)&&r.call(a),0):a.next)&&!(r=r.call(a,s[1])).done)return r;switch(a=0,r&&(s=[2&s[0],r.value]),s[0]){case 0:case 1:r=s;break;case 4:return i.label++,{value:s[1],done:!1};case 5:i.label++,a=s[1],s=[0];continue;case 7:s=i.ops.pop(),i.trys.pop();continue;default:if(!((r=(r=i.trys).length>0&&r[r.length-1])||6!==s[0]&&2!==s[0])){i=0;continue}if(3===s[0]&&(!r||s[1]>r[0]&&s[1]<r[3])){i.label=s[1];break}if(6===s[0]&&i.label<r[1]){i.label=r[1],r=s;break}if(r&&i.label<r[2]){i.label=r[2],i.ops.push(s);break}r[2]&&i.ops.pop(),i.trys.pop();continue}s=t.call(e,i)}catch(e){s=[6,e],a=0}finally{o=r=0}if(5&s[0])throw s[1];return{value:s[0]?s[1]:void 0,done:!0}}([s,n])}}};Object.defineProperty(t,"__esModule",{value:!0}),t.ConfirmModalMessage=void 0;var r=function(){function e(e,t,o,a){this.title=e,this.message=t,this.acceptText=o,this.dismissText=a,this.idMesssage="confirm-message",this.parent=document.body,this.CreateModal(),this.AppendModel()}return e.prototype.CreateModal=function(){this.modal=document.createElement("div"),this.modal.id=this.idMesssage,this.modal.classList.add("modal","modal-fixed-footer","modal-confirm-message-small");var e=document.createElement("div");e.classList.add("modal-content"),this.modal.appendChild(e);var t=document.createElement("h5");t.textContent=this.title,e.appendChild(t);var o=document.createElement("p");o.textContent=this.message,e.appendChild(o);var a=document.createElement("div");a.classList.add("modal-footer"),this.modal.appendChild(a),this.dismissButton=document.createElement("button"),this.dismissButton.classList.add("modal-action","waves-effect","waves-red","btn-flat","btn-orange"),this.dismissButton.type="button",this.dismissButton.textContent=this.dismissText,a.appendChild(this.dismissButton),this.acceptButton=document.createElement("button"),this.acceptButton.classList.add("modal-action","waves-effect","waves-red","btn-flat","btn-orange"),this.acceptButton.type="button",this.acceptButton.textContent=this.acceptText,a.appendChild(this.acceptButton)},e.prototype.AppendModel=function(){this.parent.appendChild(this.modal)},e.prototype.destroy=function(){this.modalInstance&&(this.modalInstance.close(),this.modalInstance.destroy());var e=document.querySelector("#"+this.idMesssage);e.parentNode.removeChild(e)},e.prototype.Confirm=function(){return o(this,void 0,void 0,(function(){var e=this;return a(this,(function(t){return[2,new Promise((function(t,o){(!e.modal||!e.acceptButton||!e.dismissButton)&&(e.destroy(),o("Fallo algo en la creación del mensaje")),M.Modal.init(e.modal,{dismissible:!1,opacity:.5,inDuration:300,outDuration:200,startingTop:"6%",endingTop:"8%"}),e.modalInstance=M.Modal.getInstance(e.modal),e.modalInstance.open(),e.acceptButton.addEventListener("click",(function(){t(!0),e.destroy()})),e.dismissButton.addEventListener("click",(function(){t(!1),e.destroy()}))}))]}))}))},e}();t.ConfirmModalMessage=r},"./Scripts/Shared/Http/HttpFetchService.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.HttpFetchService=void 0;var o=function(){function e(){}return e.prototype.Post=function(e,t,o){return void 0===o&&(o=!0),new Promise((function(a,r){fetch(e,{method:"post",headers:{"Content-Type":"application/json"},body:JSON.stringify(t)}).then((function(e){return o?e.json():e.text()})).then((function(e){a(e)})).catch((function(e){console.log(e),r(e)}))}))},e.prototype.PostForm=function(e,t,o,a){return void 0===a&&(a=!0),new Promise((function(r,s){fetch(e,{method:"post",headers:{RequestVerificationToken:o},body:t}).then((function(e){return a?e.json():e.text()})).then((function(e){r(e)})).catch((function(e){console.log(e),s(e)}))}))},e.prototype.PostFormURL=function(e,t,o){return void 0===o&&(o=!0),new Promise((function(a,r){fetch(e,{method:"post",headers:{"Content-Type":"application/json"},body:t}).then((function(e){return o?e.json():e.text()})).then((function(e){a(e)})).catch((function(e){console.log(e),r(e)}))}))},e}();t.HttpFetchService=o},"./Scripts/Shared/Http/index.ts":function(e,t,o){var a=this&&this.__createBinding||(Object.create?function(e,t,o,a){void 0===a&&(a=o),Object.defineProperty(e,a,{enumerable:!0,get:function(){return t[o]}})}:function(e,t,o,a){void 0===a&&(a=o),e[a]=t[o]}),r=this&&this.__exportStar||function(e,t){for(var o in e)"default"===o||Object.prototype.hasOwnProperty.call(t,o)||a(t,e,o)};Object.defineProperty(t,"__esModule",{value:!0}),t.services=void 0;var s=o("./Scripts/Shared/Http/HttpFetchService.ts");t.services=[s.HttpFetchService],r(o("./Scripts/Shared/Http/HttpFetchService.ts"),t)},"./Scripts/Shared/Models/ActionsPermission.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.ActionsPermission=void 0;t.ActionsPermission=function(){}},"./Scripts/Shared/Models/Area.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Area=void 0;t.Area=function(){}},"./Scripts/Shared/Models/Cabezote.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Cabezote=void 0;t.Cabezote=function(){}},"./Scripts/Shared/Models/Compañia.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t["Compañia"]=void 0;t["Compañia"]=function(){}},"./Scripts/Shared/Models/Conductor.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Conductor=void 0;t.Conductor=function(){}},"./Scripts/Shared/Models/Contador.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Contador=void 0;t.Contador=function(){}},"./Scripts/Shared/Models/Despacho.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Despacho=void 0;t.Despacho=function(){}},"./Scripts/Shared/Models/FechasCorteDTO.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.FechasCorteDTO=void 0;t.FechasCorteDTO=function(){}},"./Scripts/Shared/Models/Linea.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Linea=void 0;t.Linea=function(){}},"./Scripts/Shared/Models/Log.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Log=void 0;t.Log=function(){}},"./Scripts/Shared/Models/MessageResponse.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0})},"./Scripts/Shared/Models/ProcesamientoArchivos.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Paso2Mapeo=t.Paso1Mapeo=t.MapeoArchivos=void 0;t.MapeoArchivos=function(){};t.Paso1Mapeo=function(){};t.Paso2Mapeo=function(){}},"./Scripts/Shared/Models/Producto.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Producto=void 0;t.Producto=function(){}},"./Scripts/Shared/Models/Proveedor.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Proveedor=void 0;t.Proveedor=function(){}},"./Scripts/Shared/Models/Rol.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Rol=void 0;t.Rol=function(){}},"./Scripts/Shared/Models/Tanque.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Tanque=void 0;t.Tanque=function(){}},"./Scripts/Shared/Models/Terminal.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Terminal=void 0;t.Terminal=function(){}},"./Scripts/Shared/Models/Trailer.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Trailer=void 0;t.Trailer=function(){}},"./Scripts/Shared/Models/Usuario.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.Usuario=void 0;t.Usuario=function(){}},"./Scripts/Shared/Models/index.ts":function(e,t,o){var a=this&&this.__createBinding||(Object.create?function(e,t,o,a){void 0===a&&(a=o),Object.defineProperty(e,a,{enumerable:!0,get:function(){return t[o]}})}:function(e,t,o,a){void 0===a&&(a=o),e[a]=t[o]}),r=this&&this.__exportStar||function(e,t){for(var o in e)"default"===o||Object.prototype.hasOwnProperty.call(t,o)||a(t,e,o)};Object.defineProperty(t,"__esModule",{value:!0}),t.models=void 0;var s=o("./Scripts/Shared/Models/Usuario.ts"),i=o("./Scripts/Shared/Models/Cabezote.ts"),n=o("./Scripts/Shared/Models/Trailer.ts"),l=o("./Scripts/Shared/Models/Conductor.ts"),d=o("./Scripts/Shared/Models/Area.ts"),c=o("./Scripts/Shared/Models/Compañia.ts"),u=o("./Scripts/Shared/Models/Log.ts"),p=o("./Scripts/Shared/Models/Linea.ts"),m=o("./Scripts/Shared/Models/Rol.ts"),h=o("./Scripts/Shared/Models/Terminal.ts"),f=o("./Scripts/Shared/Models/Tanque.ts"),v=o("./Scripts/Shared/Models/Contador.ts"),g=o("./Scripts/Shared/Models/Proveedor.ts"),S=o("./Scripts/Shared/Models/ProcesamientoArchivos.ts"),y=o("./Scripts/Shared/Models/Producto.ts"),M=o("./Scripts/Shared/Models/Despacho.ts"),b=o("./Scripts/Shared/Models/FechasCorteDTO.ts");t.models=[s.Usuario,i.Cabezote,n.Trailer,l.Conductor,d.Area,u.Log,m.Rol,c.Compañia,h.Terminal,f.Tanque,v.Contador,p.Linea,g.Proveedor,S.MapeoArchivos,y.Producto,M.Despacho,b.FechasCorteDTO],r(o("./Scripts/Shared/Models/MessageResponse.ts"),t),r(o("./Scripts/Shared/Models/Usuario.ts"),t),r(o("./Scripts/Shared/Models/Cabezote.ts"),t),r(o("./Scripts/Shared/Models/Trailer.ts"),t),r(o("./Scripts/Shared/Models/Conductor.ts"),t),r(o("./Scripts/Shared/Models/Area.ts"),t),r(o("./Scripts/Shared/Models/Linea.ts"),t),r(o("./Scripts/Shared/Models/Compañia.ts"),t),r(o("./Scripts/Shared/Models/Rol.ts"),t),r(o("./Scripts/Shared/Models/Terminal.ts"),t),r(o("./Scripts/Shared/Models/Tanque.ts"),t),r(o("./Scripts/Shared/Models/Contador.ts"),t),r(o("./Scripts/Shared/Models/Log.ts"),t),r(o("./Scripts/Shared/Models/Proveedor.ts"),t),r(o("./Scripts/Shared/Models/ProcesamientoArchivos.ts"),t),r(o("./Scripts/Shared/Models/Producto.ts"),t),r(o("./Scripts/Shared/Models/Despacho.ts"),t),r(o("./Scripts/Shared/Models/FechasCorteDTO.ts"),t)},"./Scripts/Shared/Utils/HTMLExtensions.ts":(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),HTMLElement.prototype.on=function(e,t,o){this.addEventListener(e,(function(e){e.target.matches(t)&&o(e)}))}},"./Scripts/Shared/Utils/JQValidations.ts":(e,t,o)=>{var a=o("./node_modules/jquery/dist/jquery.js");Object.defineProperty(t,"__esModule",{value:!0}),t.JQValidations=void 0,o("./node_modules/jquery-validation/dist/jquery.validate.js"),o("./node_modules/jquery-validation-unobtrusive/dist/jquery.validate.unobtrusive.js");var r=function(){function e(){}return e.MaxFileSizeValidation=function(){a.validator.methods.maxfilesize||(a.validator.addMethod("maxfilesize",(function(e,t,o){var a=o,r=t.files[0];return!(r&&r.size/1024>a)})),a.validator.unobtrusive.adapters.add("maxfilesize",["size"],(function(e){e.rules.maxfilesize=e.params.size,e.messages.maxfilesize=e.message})))},e.AllowedExtensionsValidation=function(){a.validator.methods.allowedextensions||(a.validator.addMethod("allowedextensions",(function(e,t,o){var a=o,r=t.files[0],s=a.split(",");return!r||!!new RegExp("("+s.join("|").replace(/\./g,"\\.")+")$").test(e)})),a.validator.unobtrusive.adapters.add("allowedextensions",["exts"],(function(e){e.rules.allowedextensions=e.params.exts,e.messages.allowedextensions=e.message})))},e.NotEqualValidation=function(){a.validator.methods.notequal||(a.validator.addMethod("notequal",(function(e,t,o){var a=o,r=t.value,s=document.getElementById(a).value;return!r||r!=s})),a.validator.unobtrusive.adapters.add("notequal",["IdinputTocompare"],(function(e){e.rules.notequal=e.params.IdinputTocompare,e.messages.notequal=e.message})))},e}();t.JQValidations=r},"./Scripts/Shared/Utils/MaskFormats.ts":(e,t,o)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.MaskFormats=void 0;var a=o("./node_modules/imask/esm/index.js"),r=function(){function e(){}return e.IntegerFormat=function(){return{Selector:".formato-int",Mask:{mask:Number,scale:0,signed:!1,thousandsSeparator:",",padFractionalZeros:!1,normalizeZeros:!0,radix:".",mapToRadix:["."]}}},e.DecimalFormat=function(){return{Selector:".formato-decimal",Mask:{mask:Number,scale:2,signed:!1,thousandsSeparator:",",padFractionalZeros:!1,normalizeZeros:!0,radix:".",mapToRadix:["."]}}},e.DateShortFormat=function(){return{Selector:".formato-fecha-corta",Mask:{mask:"00{/}MMM{/}0000",overwrite:!0,autofix:!0,blocks:{MMM:{mask:a.default.MaskedEnum,enum:["ene","feb","mar","abr","may","jun","jul","ago","sep","oct","nov","dic"]}}}}},e.DateLongFormat=function(){return{Selector:".formato-fecha-larga",Mask:{mask:"00{/}MMM{/}0000{ }HH{:}TT{:}TT",overwrite:!0,autofix:!0,blocks:{MMM:{mask:a.default.MaskedEnum,enum:["ene","feb","mar","abr","may","jun","jul","ago","sep","oct","nov","dic"]},HH:{mask:a.default.MaskedRange,from:0,to:24},TT:{mask:a.default.MaskedRange,from:0,to:59}}}}},e.IntegerFormatRang=function(e,t,o){return{Selector:e,Mask:{mask:Number,min:t,max:o,scale:0,signed:!1,thousandsSeparator:",",padFractionalZeros:!1,normalizeZeros:!0,radix:".",mapToRadix:["."]}}},e.DecimalFormatRang=function(e,t,o){return{Selector:e,Mask:{mask:Number,min:t,max:o,scale:2,signed:!1,thousandsSeparator:",",padFractionalZeros:!1,normalizeZeros:!0,radix:".",mapToRadix:["."]}}},e.Alfanumerico=function(e){return{Selector:e,Mask:{mask:/^[a-z0-9]+$/i,lazy:!1}}},e.NumericoSinSignos=function(e){return{Selector:e,Mask:{mask:/^[0-9]+$/i,lazy:!1}}},e}();t.MaskFormats=r},"./Scripts/Shared/Utils/MaskFormatsManager.ts":(e,t,o)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.MaskFormatsManager=void 0;var a=o("./node_modules/imask/esm/index.js"),r=function(){function e(){this.FormatElements=new Array}return e.prototype.RegisterMasks=function(e){this.CleanAll(),this.Masks=e},e.prototype.ApplyMasks=function(){var e,t=this;this.CleanFormats(),null===(e=this.Masks)||void 0===e||e.forEach((function(e,o,a){t.ApplyFormat(e)}))},e.prototype.AddApplyMask=function(e){this.Masks.push(e),this.ApplyFormat(e)},e.prototype.UnmaskFormats=function(){var e;null===(e=this.FormatElements)||void 0===e||e.forEach((function(e,t,o){e.MaskInput.el.input.value=e.MaskInput.unmaskedValue,e.MaskInput.masked.reset()}))},e.prototype.SetUnmaskedFormValue=function(e){var t;null===(t=this.FormatElements)||void 0===t||t.forEach((function(t,o,a){e.set(t.MaskInput.el.input.name,t.MaskInput.unmaskedValue),t.MaskInput.masked.reset()}))},e.prototype.UpdateValue=function(e){var t=this;(null==e?void 0:e.length)>0&&e.forEach((function(e){var o=t.FormatElements.filter((function(t){return t.Selector===e}));(null==o?void 0:o.length)>0&&o.forEach((function(e){e.MaskInput.updateValue(),e.MaskInput.unmaskedValue=e.MaskInput.el.value}))}))},e.prototype.ApplyFormat=function(e){var t=this;document.querySelectorAll(e.Selector).forEach((function(o){t.FormatElements.push({Selector:e.Selector,MaskInput:a.default(o,e.Mask)})}))},e.prototype.CleanAll=function(){var e;(null===(e=this.Masks)||void 0===e?void 0:e.length)>0&&this.Masks.forEach((function(e,t,o){o.splice(t,1)})),this.CleanFormats()},e.prototype.CleanFormats=function(){var e;(null===(e=this.FormatElements)||void 0===e?void 0:e.length)>0&&this.FormatElements.forEach((function(e,t,o){e.MaskInput.masked.reset(),e.MaskInput.destroy,o.splice(t,1)}))},e.prototype.Destroy=function(){this.CleanAll(),this.Masks=null,this.FormatElements=null},e}();t.MaskFormatsManager=r},"./Scripts/Shared/Utils/SelectorMenu.ts":(e,t,o)=>{var a=o("./node_modules/jquery/dist/jquery.js");Object.defineProperty(t,"__esModule",{value:!0}),t.SelectorMenu=void 0;var r=function(){function e(){}return e.SeleccionEnElMenu=function(){var e=location.href,t=e.indexOf("/",8),o="a[href$='"+e.substr(t,e.length)+"']";a(".nav-list li a").removeClass("active"),a(".collapsible-body").css({display:""}),a(".listaPadre").removeClass("active"),a(o).addClass("active");var r=a(o).attr("name");a("#"+r).addClass("active"),a("#"+r+" .collapsible-body").css({display:"block"});var s=a("#"+r).attr("name");a("#"+s).addClass("active"),a("#"+s+" #"+r+" .collapsible-body").css({display:"block"}),a("#"+s+" .nivel2").css({display:"block"}),a("#"+s+" .collapsible-body").first().css({display:"block"})},e}();t.SelectorMenu=r},"./Scripts/Shared/Utils/index.ts":function(e,t,o){var a=this&&this.__createBinding||(Object.create?function(e,t,o,a){void 0===a&&(a=o),Object.defineProperty(e,a,{enumerable:!0,get:function(){return t[o]}})}:function(e,t,o,a){void 0===a&&(a=o),e[a]=t[o]}),r=this&&this.__exportStar||function(e,t){for(var o in e)"default"===o||Object.prototype.hasOwnProperty.call(t,o)||a(t,e,o)};Object.defineProperty(t,"__esModule",{value:!0}),t.components=void 0;var s=o("./Scripts/Shared/Utils/JQValidations.ts"),i=o("./Scripts/Shared/Utils/MaskFormats.ts"),n=o("./Scripts/Shared/Utils/MaskFormatsManager.ts");t.components=[s.JQValidations,i.MaskFormats,n.MaskFormatsManager],r(o("./Scripts/Shared/Utils/JQValidations.ts"),t),r(o("./Scripts/Shared/Utils/MaskFormats.ts"),t),r(o("./Scripts/Shared/Utils/MaskFormatsManager.ts"),t)},"./Scripts/Shared/index.ts":function(e,t,o){var a=this&&this.__createBinding||(Object.create?function(e,t,o,a){void 0===a&&(a=o),Object.defineProperty(e,a,{enumerable:!0,get:function(){return t[o]}})}:function(e,t,o,a){void 0===a&&(a=o),e[a]=t[o]}),r=this&&this.__exportStar||function(e,t){for(var o in e)"default"===o||Object.prototype.hasOwnProperty.call(t,o)||a(t,e,o)};Object.defineProperty(t,"__esModule",{value:!0}),r(o("./Scripts/Shared/Http/index.ts"),t),r(o("./Scripts/Shared/Models/index.ts"),t),r(o("./Scripts/Shared/Utils/HTMLExtensions.ts"),t)}},t={};function o(a){if(t[a])return t[a].exports;var r=t[a]={exports:{}};return e[a].call(r.exports,r,r.exports,o),r.exports}return o.m=e,o.x=e=>{},o.d=(e,t)=>{for(var a in t)o.o(t,a)&&!o.o(e,a)&&Object.defineProperty(e,a,{enumerable:!0,get:t[a]})},o.o=(e,t)=>Object.prototype.hasOwnProperty.call(e,t),o.r=e=>{"undefined"!=typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(e,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(e,"__esModule",{value:!0})},(()=>{var e={usuarios:0},t=[["./Scripts/Pages/Usuarios/Usuarios.ts","packages"]],a=e=>{},r=(r,s)=>{for(var i,n,[l,d,c,u]=s,p=0,m=[];p<l.length;p++)n=l[p],o.o(e,n)&&e[n]&&m.push(e[n][0]),e[n]=0;for(i in d)o.o(d,i)&&(o.m[i]=d[i]);for(c&&c(o),r&&r(s);m.length;)m.shift()();return u&&t.push.apply(t,u),a()},s=self.webpackChunkkairosv2=self.webpackChunkkairosv2||[];function i(){for(var a,r=0;r<t.length;r++){for(var s=t[r],i=!0,n=1;n<s.length;n++){var l=s[n];0!==e[l]&&(i=!1)}i&&(t.splice(r--,1),a=o(o.s=s[0]))}return 0===t.length&&(o.x(),o.x=e=>{}),a}s.forEach(r.bind(null,0)),s.push=r.bind(null,s.push.bind(s));var n=o.x;o.x=()=>(o.x=n||(e=>{}),(a=i)())})(),o.x()})()}));
//# sourceMappingURL=usuarios.js.map