//Server Main 
const WebSocket = require('ws');
const wss = new WebSocket.Server({ port: 8080 },()=>{
    console.log('Server started');
})

wss.on('connection', function connection(ws) {
   ws.on('message', (data) => {
    console.log('data recieved: ', data.toString())
    
    roomPINcode = new Array(5);
    Array(0) = data.toString();
    console.log('The roomPIN is :' ,Array(0));
    ws.send(data.toString());
   })
})

wss.on('listening',()=>{
   console.log('listening on 8080');
})
 




//Connection
wss.on("request", request => {

const connection = request.accept(null, request.origin);
    connection.on("open", () => console.log("Server opened!"))
    connection.on("close", () => console.log("Server closed!"))
    connection.on("message", message => 
    {
    const result = JSON.parse(message.utf8Data)
    
//Create Room
    if (result.method == "create") {
        const clientId = result.clientId;
        const roomPIN = roomPIN();
        const gameId = guid();
            games[gameId] = {
                "id": gameId,
                "clients": []
            }

            const payLoad = {
                "method": "create",
                "game" : games[gameId]
            }
            const con = clients[clientId].connection;
            con.send(JSON.stringify(payLoad));
    }
    
    //Join Room
    if (result.method === "join") {

        const clientId = result.clientId;
        const gameId = result.gameId;
        const game = games[gameId];
        if (game.clients.length >=4) 
        {
            //max players reach
            console.log("Max players reach")
            return;
        }
    

    //Game Start
    if (game.clients.length === 4) updateGameState();

            const payLoad = {
                "method": "join",
                "game": game
            }
        game.clients.forEach(c => {
                clients[c.clientId].connection.send(JSON.stringify(payLoad))
        })
    }
    })
})



//PlayerScore array
const playerArray = [
    {name: "Player1", score: "430", id:"player1"},
    {name: "Player2", score: "580", id:"player2"},
    {name: "Player3", score: "310", id:"player3"},
    {name: "Player4", score: "640", id:"player4"},
    {name: "Player5", score: "495", id:"player5"}
  ] 
function compare(a,b) { return b.score - a.score }
playerArray.sort(compare);
const updatePlayerScore = playerArray.map((name, score) => {
    return name + score
})
console.log(" ", updatePlayerScore)
console.log(" ", playerArray)

//Test for leaderboard
/*var playerScore = [2300, 2600, 1230, 680, 230, 1230]
playerScore.sort(function(a, b){return a - b});
console.log("Leaderboard test: ", playerScore)*/

function S4() {
    return (((1+Math.random())*0x10000)|0).toString(16).substring(1); 
}

const guid = () => (S4() + S4() + "-" + S4() + "-4" + S4() + "-" + S4()).toLowerCase();