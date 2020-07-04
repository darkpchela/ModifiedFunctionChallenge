let chart = null;

class ChartWindow extends React.Component {
    constructor(props) {
        super(props);
    }

    render(){
        return (
            <div className="chart-container">
                <canvas id={this.props.chartId}></canvas>
                </div>
        );
    }
    componentDidMount() {
        let ctx = document.getElementById(this.props.chartId);
        chart = new Chart(ctx,
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
    }
}

class ListItem extends React.Component {
    render() {
        return <li>{this.props.text}</li>;
    }
}
class ErrorList extends React.Component {
    render() {
        return (
            <div>
                <ul className="text-danger">
                    {
                        this.props.data.map(function (item) {
                            return <ListItem key={item} text={item} />
                        })
                    }
                </ul>
            </div>
            );
    }
}

class ChartInputForm extends React.Component {

    constructor(props) {
        console.log("construcor");
        super(props);
        this.btnSubmitOnClick = this.btnSubmitOnClick.bind(this);
        this.validateForm = this.validateForm.bind(this);
        this.onInputChange = this.onInputChange.bind(this);
    }

    componentWillMount() {
        this.setState({ errors: [] });
    }
    onInputChange(e) {
        let id = e.target.id;
        let inputValue = e.target.value.toString();
        if (inputValue.length > 3) {
            $("#" + id).val(inputValue.substr(0, 3));
        }
    }
    validateForm() {
        let errors = [];
        let aVal = $("#a").val();
        let bVal = $("#b").val();
        let cVal = $("#c").val();
        let stepVal = $("#step").val();
        let fromVal = $("#from").val();
        let toVal = $("#to").val();
        if (aVal < -100 || aVal > 100) {
            errors.push("'a' must be between -100 and 100");
        }
        if (bVal < -100 || bVal > 100) {
            errors.push("'b' must be between -100 and 100");
        }
        if (cVal < -100 || cVal > 100) {
            errors.push("'c' must be between -100 and 100");
        }
        if (stepVal < 1 || stepVal > 100) {
            errors.push("'Step' must be between 1 and 100");
        }
        if (fromVal < -100 || fromVal > 100) {
            errors.push("'From' must be between -100 and 100");
        }
        if (toVal < -100 || toVal > 100) {
            errors.push("'To' must be between -100 and 100");
        }
        if (fromVal >= toVal) {
            errors.push("value of 'From' must be grater then value of 'To'");
        }
        if (stepVal >= (toVal - fromVal)) {
            errors.push("value of 'Step' must be greater, then difference of 'From' and 'To'")
        }
        if (errors.length > 0) {
            this.setState({ errors: errors });
            return false;
        }
        else {
            this.setState({ errors: [] });
            return true;
        }
    }

    btnSubmitOnClick(e) {
        e.preventDefault();
        if (this.validateForm()) {
            let data = $('#form').serialize();
            $.ajax({
                method: 'POST',
                data: data,
                url: '/Home/FunctionAjax/',
                success: function (points) {
                    if (points !== null) {
                        chart.data.labels = [];
                        chart.data.datasets[0].data = [];
                        for (let i = 0; i < points.length; i++) {
                            chart.data.labels.push(points[i].x);
                            chart.data.datasets[0].data.push(points[i].y);
                        }
                        chart.update();
                    }
                }
            });
        }
    }

    render() {
        return (
            <div>
                <form id="form">
                    <label className="title">Function: y = ax^2 + bx + c</label>
                    <div className="flex-row">
                        <div>
                            <label>a:</label> <input type="number" id="a" name="a" defaultValue="5" onChange={this.onInputChange} />
                        </div>
                        <div>
                            <label>b:</label><input type="number" id="b" name="b" defaultValue="5" onChange={this.onInputChange}  />
                        </div>
                        <div>
                            <label>c:</label><input type="number" id="c" name="c" defaultValue="16" onChange={this.onInputChange}  />
                        </div>
                        <div>
                            <label>Step:</label><input type="number" id="step" name="step" min="1" defaultValue="1" onChange={this.onInputChange}  />
                        </div>
                        <div>
                            <label>From:</label> <input type="number" id="from" name="from" defaultValue="-10" onChange={this.onInputChange} />
                        </div>
                        <div>
                            <label>To:</label><input type="number" id="to" name="to" defaultValue="10" onChange={this.onInputChange} />
                        </div>
                    </div>
                    <button id="btn" type="submit" onClick={this.btnSubmitOnClick} >Plot</button>
                    <div id="errorDiv">
                        {(this.state.erorrs === null) ? (null) : (< ErrorList data={this.state.errors} />)}
                    </div>
                </form>
            </div>
        );
    }
}

ReactDOM.render(
    <ChartWindow chartId="chart" />,
    document.getElementById("chart-box")
);
ReactDOM.render(
    <ChartInputForm />,
    document.getElementById("input-box")
);