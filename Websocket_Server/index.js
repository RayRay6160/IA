//Server Main 
const WebSocket = require('ws');
const wss = new WebSocket.Server({ port: 8080 },()=>{
    console.log('server started');
})

wss.on('connection', function connection(ws) {
   ws.on('message', (data) => {
    console.log('data recieved: ', data.toString())
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
        if (game.clients.length > 4) 
        {
            //max players reach
            return;
        }
    

    //Game Start
    if (game.clients.length === 3) updateGameState();

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

//generate gamePIN
function roomPIN() {
    return Math.random().toString(4).substring(1); 
}

//Sync
function Sync() {
    ws.Sync(PlayerData);
}

//Leaderboard
function Leaderboard(unity) {
    const leaderboard = new leaderboard;
}

function S4() {
    return (((1+Math.random())*0x10000)|0).toString(16).substring(1); 
}

const guid = () => (S4() + S4() + "-" + S4() + "-4" + S4() + "-" + S4()).toLowerCase();