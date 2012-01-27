(function($){
	$.fn.placeholder = function(){

		function valueIsPlaceholder(input){
			return ($(input).val() == $(input).attr("placeholder"));
		}
		return this.each(function(){

			$(this).find(':input').each(function(){

				if($(this).attr("type") == "password"){
					
					var new_field = $("<input type='text'>");
					new_field.attr("rel", $(this).attr("id"));
					new_field.attr("value", $(this).attr("placeholder"));
					$(this).parent().append(new_field);
					new_field.hide();
					
					function showPasswordPlaceHolde(input){
						if( $(input).val() == "" || valueIsPlaceholder(input) ){
							$(input).hide();
							$('input[rel=' + $(input).attr("id") + ']').show();
						};
					};

					new_field.focus(function(){
						$(this).hide();
						$('input#' + $(this).attr("rel")).show().focus();
					});

					$(this).blur(function(){
						showPasswordPlaceHolder(this, false);
					});

					showPasswordPlaceHolder(this);
				};
			});
		});
})(jQuery);
