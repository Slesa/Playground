function tabs(pages) {
	if (!pages.length) return;
	pages.addClass("dyn-tabs");
	pages.first().show();
	var tabNavigation = $('<ul id="tabs" />').insertBefore(pages.first());
	pages.each(function(){
		var listElement = $("<li />");
		var label = $(this).attr("title") ? $(this).attr("title") : "Kein label";
		listElement.text(label);
		tabNavigation.append(listElement)
	});
	var items = tabNavigation.find("li");
	items.first().addClass("current");
	items.click(function(){
		items.removeClass("current");
		$(this).addClass("current");
		pages.hide();
		pages.eq($(this).index()).fadeIn("slow");
	});
}

$(document).ready(function(){
	tabs($("div.tabs"));
});
