function mask_phone(element){
	var SPMaskBehavior = function (val) {
	    return val.replace(/\D/g, '').length === 11 ? '(00)00000-0000' : '(00)0000-00009';
	},
	spOptions = {
	    onKeyPress: function(val, e, field, options) {
	        field.mask(SPMaskBehavior.apply({}, arguments), options);
	    }
	};

	$(element).mask(SPMaskBehavior, spOptions);	
}
function mask_phone_fixo(element){	
	$(element).mask('(00)0000-0000');	
}

function mask_coin(element){
	$(element).mask('#.##0,00', {reverse: true});
}

function mask_zipcode(element){
	$(element).mask('00.000-000');
}

function mask_cpf(element){
	$(element).mask('000.000.000-00', {reverse: true, placeholder: '___.___.___-__'});
}

function mask_cnpj(element){
	 $(element).mask('00.000.000/0000-00', {reverse: true, placeholder: '__.___.___/____-__'});
}

function readImage(f, element) {	
    if (f.files && f.files[0]) {
        var file = new FileReader();
        file.onload = function(e) {
            document.getElementById(element).src = e.target.result;
            document.getElementById(element).width = 180;
            document.getElementById(element).height = 195;
        };       
        file.readAsDataURL(f.files[0]);
    }
}
