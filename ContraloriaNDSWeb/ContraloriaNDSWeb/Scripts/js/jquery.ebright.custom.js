$(document).ready(function() {
	//Carousels
	$('.carousel').carousel({
		interval: 5000,
		pause	: 'hover'
	});
	// Sortable list
	$('#ulSorList').mixitup();
	// Fancybox
	$(".theater").fancybox();
	// Fancybox	
	$(".ext-source").fancybox({
		'transitionIn'		: 'none',
		'transitionOut'		: 'none',
		'autoScale'     	: false,
		'type'				: 'iframe',
		'width'				: '50%',
		'height'			: '60%',
		'scrolling'   		: 'no'
	});
	// Masonry
	var container = document.querySelector('#masonryWr');
	var msnry = new Masonry( container, {
	  itemSelector: '.item'
	});
	// Scroll to top
	$().UItoTop({ easingType: 'easeOutQuart' });
	//Animate hover slide
	$( ".animate-hover-slide figure" ).each(function() {
		var animateImgHeight = $(this).find("img").height();
		$(this).find("figcaption").css({"height":animateImgHeight+"px"});
	});
	// Search function
	$("#cmdSearch, #cmdSearchCollapse").click(function(){
		$("#divSearch").fadeIn(300);
		return false;	
	});
	$("#cmdCloseSearch").click(function(){
		$("#divSearch").fadeOut(300);	
	});
	
	// Keyboard shortcuts
	$('html').keyup(function(e){
		if(e.keyCode == 27)
			if($("#divSearch").is(':visible'))
				$("#divSearch").fadeOut(300);
	});
	// Scheme Switcher
	var scheme = $.cookie('scheme');
	if (scheme == 'one') {
		$("#scheme").attr("href","css/skin-one.css");
	}
	else if (scheme == 'two') {
		$("#scheme").attr("href","css/skin-two.css");
	}
	else if (scheme == 'three') {
		$("#scheme").attr("href","css/skin-three.css");
	}
	else if (scheme == 'four') {
		$("#scheme").attr("href","css/skin-four.css");
	}
	else if (scheme == 'five') {
		$("#scheme").attr("href","css/skin-five.css");
	}
	else if (scheme == 'default') {
		$("#scheme").attr("href","");
	}
	else if (scheme == 'black') {
		$("#navbar").removeClass("navbar-white");
		$("#navbar").addClass("navbar-default");
		$("#logo").attr("src", "images/eBright-logo.png");
		$("#scheme").attr("href","");
		$.cookie('scheme', 'black', { expires: date});
	}
	else if (scheme == 'white') {
		$("#navbar").removeClass("navbar-default");
		$("#navbar").addClass("navbar-white");
		$("#logo").attr("src", "images/eBright-logo-b.png");
		$("#scheme").attr("href","css/skin-four.css");
		$.cookie('scheme', 'white', { expires: date});
	}
	
	var date = new Date();
	date.setTime(date.getTime() + (5 * 60 * 1000));
	
	$("#btnBlack").click(function(){
		$("#navbar").removeClass("navbar-white");
		$("#navbar").addClass("navbar-default");
		$("#logo").attr("src", "images/eBright-logo.png");
		$("#scheme").attr("href","");
		$.cookie('scheme', 'black', { expires: date});
		return false;
	});
	
	$("#btnWhite").click(function(){
		$("#navbar").removeClass("navbar-default");
		$("#navbar").addClass("navbar-white");
		$("#logo").attr("src", "images/eBright-logo-b.png");
		$("#scheme").attr("href","css/skin-four.css");
		$.cookie('scheme', 'white', { expires: date});
		return false;
	});
	
	$("#btnSchemeDefault").click(function(){
		$("#scheme").attr("href","");
		$.cookie('scheme', 'default', { expires: date});
		return false;
	});
	$("#btnSchemeOne").click(function(){
		$("#scheme").attr("href","css/skin-one.css");
		$.cookie('scheme', 'one', { expires: date});
		return false;
	});
	$("#btnSchemeTwo").click(function(){
		$("#scheme").attr("href","css/skin-two.css");
		$.cookie('scheme', 'two', { expires: date});
		return false;
	});
	$("#btnSchemeThree").click(function(){
		$("#scheme").attr("href","css/skin-three.css");
		$.cookie('scheme', 'three', { expires:date});
		return false;
	});
	$("#btnSchemeFour").click(function(){
		$("#scheme").attr("href","css/skin-four.css");
		$.cookie('scheme', 'four', { expires:date});
		return false;
	});
	$("#btnSchemeFive").click(function(){
		$("#scheme").attr("href","css/skin-five.css");
		$.cookie('scheme', 'five', { expires:date});
		return false;
	});
	
});

$(window).resize(function(){
	//Animate hover slide
	$( ".animate-hover-slide figure" ).each(function() {
		var animateImgHeight = $(this).find("img").height();
		$(this).find("figcaption").css({"height":animateImgHeight+"px"});
	});
});