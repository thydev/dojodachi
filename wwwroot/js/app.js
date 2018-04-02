$(document).ready(function(){
    console.log("Ready");

    // var apiUrl = "http://pokeapi.salestock.net/api/v2/pokemon/" + $(this).attr("id")
    //     $.get(apiUrl, function(res) {
    //         //console.log(res);
    //         var types = "<ul>";
    //         for (var i = 0; i < res.types.length; i++){
    //             types += "<li>" + res.types[i].type.name + "</li>";
    //         }
    //         types += "</ul>"
    //         $("#detail").html(
    //             "<h1>" + res.name + "</h1>"
    //             + "<img src='" + res.sprites.front_default + "'" + " >"
    //             + "<h3>Types:</h3> " + types + ""
    //             + "<h3>Height: " + res.height + "</h3>"
    //             + "<h3>Weight: " + res.weight + "</h3>"
    //         );
    //     }, "json");
    $("#feed").click(function(e){
        console.log("feed");
        e.preventDefault();

        var apiUrl = "/actions/feed";
        $.get(apiUrl, function(res) {
            console.log(res);
            updateInformation(res.dachi);
        }, "json");
    });

    $("#play").click(function(e){
        console.log("play");
        e.preventDefault();
    });

    $("#work").click(function(e){
        console.log("work");
        e.preventDefault();
    });

    $("#sleep").click(function(e){
        console.log("sleep");
        e.preventDefault();
    });

    function updateInformation(dachi){
        $(".fullness").text(dachi.fullness);
        $(".happiness").text(dachi.happiness);
        $(".meals").text(dachi.meals);
        $(".energy").text(dachi.energy);
    }
});