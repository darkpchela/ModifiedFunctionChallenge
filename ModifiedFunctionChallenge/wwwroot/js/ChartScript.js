let myChartName = "chart";
let ctx = document.getElementById(myChartName);
let my_chart = new Chart(ctx,
    {
        type: "line",
        data:
        {
            labelsp: [],
            datasets:
                [{
                    label: 'f(x) = ax^2 + bx + c',
                    data: [],
                    borderColor: 'blue',
                    borderWidth: 2,
                    fill: false
                }]
        },
        options:
        {
            responsitive: true,
            scales:
            {
                xAxes:
                    [{
                        display: true
                    }],
                yAxes:
                    [{
                        display: true
                    }]
            }
        }
    });

let setNewChartData = function (pointsJson) {
    let points = JSON.parse($("#pointsJSON").val()) ?? [];
    my_chart.data.labels = [];
    my_chart.data.datasets[0].data = [];
    for (let i = 0; i < points.length; i++) {
        my_chart.data.labels.push(points[i].x);
        my_chart.data.datasets[0].data.push(points[i].y);
    }
    my_chart.update();
}

$(function () {
    if ($("#pointsJSON").val().length > 0) {
        setNewChartData($("#pointsJSON").val());
        //let points = JSON.parse($("#pointsJSON").val()) ?? [];
        //my_chart.data.labels = [];
        //my_chart.data.datasets[0].data = [];
        //for (let i = 0; i < points.length; i++) {
        //    my_chart.data.labels.push(points[i].x);
        //    my_chart.data.datasets[0].data.push(points[i].y);
        //}
        //my_chart.update();
    }

});