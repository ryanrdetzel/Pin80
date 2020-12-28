

gameList.on("gameselect", ev => {
    const gameId = ev.game.id;
    const gameInfo = gameList.getGameInfo(gameId);
    const name = gameInfo.displayName;

    logfile.log("gameselect",  JSON.stringify(name));
})

mainWindow.on("dofevent", ev => { 
    //logfile.log("DOF Event", JSON.stringify(ev))

    switch(ev.name){
        case "PBYGameSelect":
            if (ev.value === 1){
                // Get game info and issue command
                const currentGame = gameList.getWheelGame(0);
                const gameInfo = getGameContent(currentGame.id);

                sendCommand("PBYGameSelect " + gameInfo.rom);
            }
            break;
    }
 });

 function getGameContent(gameId){
    const gameInfo = gameList.getGameInfo(gameId);
    return {
        "name": gameInfo.displayName,
        "rom": gameInfo.resolveROM().dofRom,
    }
 }

 function sendCommand(command){
    logfile.log("Sending ", command);

    let request = new HttpRequest();
    const url = "http://127.0.0.1:8012";
    request.open("GET", url + "/?cmd=" + command + '&cb=' + Math.random(), true);
    request.send().then(function(reply) {
        logfile.log("Success " + reply);
    }).catch(function(error) {
        logfile.log("Error");
    });
 }