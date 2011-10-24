function menu(obj){
	if (!obj.length) return;
	$(obj).find("ul").css({display: "none"});
	$(obj).hover(function(){
		$(this).find('ul').first().stop(true, true).slideDown(1000);
	},function(){
		$(this).find('ul').first().stop(true, true).slideUp(1000);
	});
}

$(document).ready(function(){
	menu($("ul li"));
});

