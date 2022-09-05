//import package
const WebSocket = require('ws')

//Create the WebSocket server
const wss = new WebSocket.WebSocketServer({ port: 8080 }, () => {
    console.log('server started')
})

//Object that stores player data
class Player {
    constructor(name, score) {
        this.name = name
        this.score = score
    }
}

class Room {

    constructor(pinCode, hostName) {
        this.pinCode = pinCode
        this.p1 = new Player(hostName, 0)
        this.p2 = new Player()
        this.p3 = new Player()
        this.p4 = new Player()
    }
}

const roomList = []

function findRoom(pin) {
    return roomList.find((e) => e.pinCode === pin)
}

//=====WEBSOCKET FUNCTIONS======

//Websocket function that managages connection with clients
wss.on('connection', function connection(client) {

    console.log(`Client Connected!`)

    //Method retrieves message from client
    client.on('message', (data) => {

        //parsing the data received from our client/player from a JSON string into a JSON object
        var dataJSON = JSON.parse(data)

        console.log('RequestType : ' + dataJSON.requestType);

        //Identify the request type of message
        switch (dataJSON.requestType) {

            //Create room
            case 0:

                //Check if pin code haven't create yet
                for (let i = 0; i < roomList.length; i++) {
                    if (roomList[i].pinCode == dataJSON.pinCode) {
                        client.send('{"result": false}')
                        return;
                    }
                }

                roomList.push(new Room(dataJSON.pinCode, dataJSON.playerName)) //Register the pin code

                console.log(roomList[roomList.length - 1])

                var jResult = { result: true, PIN: roomList[roomList.length - 1].pinCode }

                //Return the result whether create room is/not success
                client.send(JSON.stringify(jResult))

                break;

            //Join room
            case 1:

                var roomData = findRoom(dataJSON.pinCode)

                //Undefined PIN code
                if (roomData == undefined) {
                    var jRoom = { result: false, PIN: dataJSON.pinCode }
                    client.send(JSON.stringify(jRoom))

                    return;
                }

                findRoom(dataJSON.pinCode).p2.name = dataJSON.playerName

                console.log(findRoom(dataJSON.pinCode))

                var jRoom = { result: true, PIN: roomData.pinCode }

                client.send(JSON.stringify(jRoom))

                break;

            //Gameplay
            case 2:
                break;

            //EndGame
            case 3:
                break;
        }
    })

    //Method notifies when client disconnects
    client.on('close', () => {
        console.log('This Connection Closed!')
        console.log("Removing Client: " + client.id)
    })

})

wss.on('listening', () => {
    console.log('listening on 8080')
})