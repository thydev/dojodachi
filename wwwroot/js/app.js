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
            updateInformation(res);
        }, "json");
    });

    $("#play").click(function(e){
        console.log("play");
        e.preventDefault();

        var apiUrl = "/actions/play";
        $.get(apiUrl, function(res) {
            console.log(res);
            updateInformation(res);
        }, "json");
    });

    $("#work").click(function(e){
        console.log("work");
        e.preventDefault();

        var apiUrl = "/actions/work";
        $.get(apiUrl, function(res) {
            console.log(res);
            updateInformation(res);
        }, "json");
    });

    $("#sleep").click(function(e){
        console.log("sleep");
        e.preventDefault();

        var apiUrl = "/actions/sleep";
        $.get(apiUrl, function(res) {
            console.log(res);
            updateInformation(res);
        }, "json");
    });

    function updateInformation(res){
        $(".fullness").text(res.dachi.fullness);
        $(".happiness").text(res.dachi.happiness);
        $(".meals").text(res.dachi.meals);
        $(".energy").text(res.dachi.energy);
        $(".reaction").text(res.reaction);
        gameStatus(res.dachi);
    }

    // If energy, fullness, and happiness are all raised to over 100, you win! a restart button should be displayed.
    // If fullness or happiness ever drop to 0, you lose, and a restart button should be displayed.
    function gameStatus(dachi){
        if(dachi.energy >= 100 && dachi.fullness >= 100 && dachi.happiness >= 100){
            console.log("Win");
            $(".reaction").text("You won !");
            $("#feed").hide();
            $("#play").hide();
            $("#work").hide();
            $("#sleep").hide();
            $("#restart").show();
        } else if (dachi.fullness <= 0 || dachi.happiness <= 0){
            console.log("Loss");
            $(".reaction").text("You lost !");
            $("#feed").hide();
            $("#play").hide();
            $("#work").hide();
            $("#sleep").hide();
            $("#restart").show();
        }
    }
});