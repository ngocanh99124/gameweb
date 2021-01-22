var canvas = document.getElementById("canvas"),
    ctx = canvas.getContext("2d");
canvas.width = 1900; canvas.height = 900;
var girl = "girl/", boy = "boy/";
var source_ = "/Content/image/";
var left = 1, right = -1;
var Idle = 0, Run = 1, Attack = 2, Jump = 3, Dead = 4, Hurt = 5;
var width = [145, 188, 262, 200];
var height = [250, 260, 282, 221];
var sl = [9, 9, 9, 9];
var name_ = ["Idle__", "Run__", "Attack__", "Jump__"];
var speed = 10, high = 45, change_image = 10, read_key = 10, blood_size = 10, on_game = 1;
var socket = new WebSocket("ws://localhost:8080/WebApplication5/endpoint");
socket.onopen = function () { processOnOpen(); }
socket.onclose = function () { processOnClose(); }
function processOnClose() {
    console.log("close to server!");
}
function processOnOpen() {
    console.log("connect connect!");
    var id = String(document.getElementById("user").value);
    var a = 0;
    if (player1.avt == "girl/")
        a = 1;
    var json = { "status": 0, "player": id, "avt": a };
    var mess = JSON.stringify(json);
    socket.send(mess);
}

var list=[];

socket.onmessage = function (mess) {
    var message = mess.data;
    var json = JSON.parse(message);
    list.push(json);
}

var player1 = {
    x: 50,
    y: canvas.height - height[Idle]-50,
    w: width[Idle],
    h: height[Idle],
    avt: girl,
    image: "",
    direct: left,
    status: Idle,
    sl: 0,
    blood:1000
}

var player2 = {
    x: canvas.width - width[Idle]-50,
    y: canvas.height - height[Idle] - 50,
    w: width[Idle],
    h: height[Idle],
    avt: boy,
    image: "",
    direct: right,
    status: Idle,
    sl: 0,
    blood:1000
}

document.getElementById("blood1").value = String(player1.blood);
document.getElementById("blood2").value = String(player2.blood);



var keys = [];

function update() {
    if (on_game == 0)
        return;
    if (player1.status == Jump) {
        setTimeout(update, read_key);
        return;
    }
    if (keys[38]) {
        if (player1.status != Jump) {
            player1.status = Jump;
            player1.sl = 0;
        }
    }
    else {
        if (keys[39]) {
            if (player1.direct == left) {
                player1.status = Run;
                player1.sl = 0;
            }
            else {
                player1.direct = left;
                player1.status = Run;
                player1.sl = 0;
            }
        }
        else {
            if (keys[37]) {
                if (player1.direct == right) {
                    player1.status = Run;
                    player1.sl = 0;
                }
                else {
                    player1.direct = right;
                    player1.status = Run;
                    player1.sl = 0;
                }
            }
            else {
                if (keys[40]) {
                    player1.status = Attack;
                    player1.sl = 0;
                    if (check) {

                    }
                }
            }
        }
    }

    setTimeout(update, read_key);
}


function draw(src, x, y, w, h) {
    var image = new Image();
    image.src = src;
    image.onload = function () {
        ctx.drawImage(image, x, y, w, h);
    }
}


function manager() {
    if (on_game == 0)
        return;
    ctx.clearRect(0, 0, canvas.width, canvas.height);
    var c;
    if (player1.direct == left) c = "0"; else c = "1";
    player1.image = source_ + player1.avt + name_[player1.status] + c + "0" + (player1.sl).toString() + ".png";
    player1.w = width[player1.status];
    player1.h = height[player1.status];
    draw(player1.image, player1.x, player1.y, player1.w, player1.h);
    player1.sl += 1;
    if (player1.sl >= sl[player1.status]) {
        player1.status = Idle;
        player1.sl = 0;
    }
    if (player1.status == Jump) {
        if (player1.sl <= 4)
            player1.y = player1.y - high;
        else
            player1.y = player1.y + high;
    }
    if (player1.status == Run) {
        if (player1.direct == left) {
            if (player1.x + player1.w + speed <= canvas.width) {
                player1.x = player1.x + speed;
            }
            else {
                player1.status = Idle;
                player1.sl = 0;
            }
        }
        else {
            if (player1.x - speed >= 0) {
                player1.x = player1.x - speed;
            }
            else {
               player1.status = Idle;
               player1.sl = 0;
            }
        }
    }
    if (list.length != 0) {
        var json = list.pop();
        player2.x = canvas.width - json.w - json.x;
        player2.y = canvas.height - json.h - json.y;
        player2.w = json.w;
        player2.h = json.h;
        player2.avt = json.avt;
        player2.sl = json.sl;
        player2.status = json.action;
        player2.direct = -json.direct;
        player2.blood = json.blood;
    }
   
    if (player2.direct == left) c = "0"; else c = "1";
    player2.image = source_ + player2.avt + name_[player2.status] + c + "0" + (player2.sl).toString() + ".png";
    draw(player2.image, player2.x, player2.y, player2.w, player2.h);
    sendMessage();
    calc();
    setTimeout(manager, change_image);
}

function checkRec(x,  y, w, h, xx, yy, ww, hh) {
    
    if (w + x > ww + xx) w = ww + xx;
    else w = w + x;
    if (h + y > hh + yy) h = hh + yy;
    else h = h + y;
    if (x < xx) x = xx;
    if (y < yy) y = yy;
    var dt = (w - x) * (h - y);
    if (dt >= 10) return true;
    return false;
}

function calc() {
    var x = player1.x, y = player1.y, w = player1.w, h = player1.h,
        xx = player2.x, yy = player2.y, ww = player2.w, hh = player2.h;
    var check = check(x, y, w, h, xx, yy, ww, hh);
    var player = String(document.getElementById("user").value);
    var json = { "status": 4, "player": player };
    var mess = JSON.stringify(json);
    if (player1.status == Attack) {
        if (check) {
            player2.blood -= blood_size;
            socket.send(mess);
        }
    }
    if (player2.status == Attack)
        player1.blood -= blood_size;
    document.getElementById("blood1").value = String(player1.blood);
    document.getElementById("blood2").value = String(player2.blood);
    if (player1.blood == 0) {
        document.getElementById("game").value = "Game over!";
        socket.close();
        on_game = 0;
    }
    if (player2.blood == 0) {
        document.getElementById("game").value = "You win!";
        socket.close();
        on_game = 0;
    }
}

manager();

function sendMessage() {
    var player = String(document.getElementById("user").value);
    var x = player1.x, y = player1.y, w = player1.w, h = player1.h, avt = player1.avt, sl = player1.sl, direct = player1.direct, blood = player1.blood, action = player1.status;
    var json = { "status": 3, "player": player, "x": x, "y": y, "w": w, "h": h, "avt": avt, "sl": sl, "direct": direct, "blood": blood, "action": action };
    var mess = JSON.stringify(json);
    socket.send(mess);
}

update();

document.body.addEventListener("keydown", function (e) {
    keys[e.keyCode] = true;
});
document.body.addEventListener("keyup", function (e) {
    keys[e.keyCode] = false;
});