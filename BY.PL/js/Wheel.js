var colors = ["#000000", "#e83e8c", "#ffb320", "#6f42c1", "#28a745", "#ffc107", "#000000", "#6610f2"];
// NEED to pre load this data prior
var prdesc = [];

$(document).ready(function () {
    $.getJSON("/Wheel/WheelList", function (data) {
        $.each(data, function (i, item) {
            prdesc.push(item.Description)
        });
    });
});
var prize_descriptions = prdesc;
var current_user_status = {};

var startAngle = 0;
var arc = Math.PI / 4;
var spinTimeout = null;

var spinArcStart = 10;
var spinTime = 0;
var spinTimeTotal = 0;

var current_user_status = null;
var spin_results = null;

var wheel;

var counter, tt;

function drawSpinnerWheel() {
    var canvas = document.getElementById("canvas");
    if (canvas.getContext) {
        var outsideRadius = 200;
        var textRadius = 160;
        var insideRadius = 125;

        wheel = canvas.getContext("2d");
        wheel.clearRect(0, 0, 500, 500);


        wheel.strokeStyle = "#ecf0f1";
        wheel.lineWidth = 5;

        wheel.font = '17px Helvetica, Arial';

        for (var i = 0; i < 8; i++) {
            var angle = startAngle + i * arc;
            wheel.fillStyle = colors[i];

            wheel.beginPath();
            wheel.arc(250, 250, outsideRadius, angle, angle + arc, false);
            wheel.arc(250, 250, insideRadius, angle + arc, angle, true);
            wheel.stroke();
            wheel.fill();

            wheel.save();
            wheel.shadowOffsetX = -1;
            wheel.shadowOffsetY = -1;
            wheel.shadowBlur = 0;
            wheel.shadowColor = "rgb(220,220,220)";
            wheel.fillStyle = "#ecf0f1";
            wheel.translate(250 + Math.cos(angle + arc / 2) * textRadius, 250 + Math.sin(angle + arc / 2) * textRadius);
            wheel.rotate(angle + arc / 2 + Math.PI / 2);
            var text = prize_descriptions[i];
            if (text === undefined) text = "ÇEVİR";
            wheel.fillText(text, -wheel.measureText(text).width / 2, 0);
            wheel.restore();
        }

        //Arrow
        wheel.fillStyle = "#ecf0f1";
        wheel.beginPath();
        wheel.moveTo(250 - 4, 250 - (outsideRadius + 5));
        wheel.lineTo(250 + 4, 250 - (outsideRadius + 5));
        wheel.lineTo(250 + 4, 250 - (outsideRadius - 5));
        wheel.lineTo(250 + 9, 250 - (outsideRadius - 5));
        wheel.lineTo(250 + 0, 250 - (outsideRadius - 13));
        wheel.lineTo(250 - 9, 250 - (outsideRadius - 5));
        wheel.lineTo(250 - 4, 250 - (outsideRadius - 5));
        wheel.lineTo(250 - 4, 250 - (outsideRadius + 5));
        wheel.fill();
    }
}

function spin() {
    $("#spin").unbind('click');
    $("#spin").attr("id", "nospin");

    //document.getElementById('timer').innerHTML = " ";
    document.getElementById('category').innerHTML = " ";

    spinMovement = Math.floor(Math.random() * 20) + prize_descriptions.length * 2;

    spinAngleStart = 1 * 10 + spinMovement;
    spinTime = 0;
    spinTimeTotal = Math.floor(Math.random() * 4) * Math.floor(Math.random() * 6) + Math.floor(Math.random() * 8) * Math.floor(Math.random() * 2000) + 2000;

    console.log(spinMovement + " - " + spinTimeTotal);

    rotateWheel();
}

function rotateWheel() {
    spinTime += 30;
    if (spinTime >= spinTimeTotal) {
        stopRotateWheel();
        return;
    }
    var spinAngle = spinAngleStart - easeOut(spinTime, 0, spinAngleStart, spinTimeTotal);
    startAngle += (spinAngle * Math.PI / 180);
    drawSpinnerWheel();
    spinTimeout = setTimeout('rotateWheel()', 30);
}

function stopRotateWheel() {
    clearTimeout(spinTimeout);
    var degrees = startAngle * 180 / Math.PI + 90;
    var arcd = arc * 180 / Math.PI;
    var index = Math.floor((360 - degrees % 360) / arcd);
    wheel.save();
    wheel.font = '30px "Homestead-Inline", Helvetica, Arial';
    var text = prize_descriptions[index];
    wheel.fillText(text, 250 - wheel.measureText(text).width / 2, 250 + 10);
    wheel.restore();
    //document.getElementById('timer').innerHTML = "15";
    document.getElementById('category').innerHTML = " " + text;
    //counter = 15;

    $.ajax({
        url: "/Wheel/UserTrans",
        type: "POST",
        dataType: "json",
        data: { desc: text },
        success: function (data) {
            console.log(data);

        }
    });


    tt = setInterval(function () { startTime() }, 1000);
}

function easeOut(t, b, c, d) {
    var ts = (t /= d) * t;
    var tc = ts * t;
    return b + c * (tc + -3 * ts + 3 * t);
}

drawSpinnerWheel();

function startTime() {
    if (counter == 0) {
        clearInterval(tt);

        $("#nospin").attr("id", "spin");
        $("#spin").bind('click', function (e) {
            e.preventDefault();
            spin();
        });

    } else {
        counter--;
    }
    //document.getElementById('timer').innerHTML = counter;
}

$("#spin").bind('click', function (e) {
    e.preventDefault();
    spin();
});