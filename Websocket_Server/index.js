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

/*wss.on('connection', ws =>{
    ws.on("message", message => {
        const data = JSON.parse(message);
        console.log(data.x, data.y);
    } )
});*/

wss.on('listening',()=>{
   console.log('listening on 8080');
})

//Create Room
function Create() {
    const roomPIN = new roomPIN;
    const rand = Math.random().toString().substr(2, 8);
    roomPIN = rand;
    console.log("Your PIN is: "+ roomPIN);
    Socket.send(roomPIN);
}  

//Join Room
function Join() {
    const InputPIN = prompt("Please input the PIN:");
}

//Sync
function Sync() {
    ws.Sync(PlayerData);
}

//Leaderboard
function Leaderboard(unity) {
    const leaderboard = new leaderboard;

}

