import React, { Component } from 'react';

export class ResultBody extends Component {
    constructor(props) {
        super(props);
        this.months = [];
        this.chartData = [];
        this.methodname = null;
        this.getAllMonths = this.getAllMonths.bind(this);
        this.checkValueExist = this.checkValueExist.bind(this);
        this.setChartParameters = this.setChartParameters.bind(this);
        this.getAmountOwed = this.getAmountOwed.bind(this);
        this.getBillTypes = this.getBillTypes.bind(this);
        this.checkElementsHasClassName = this.checkElementsHasClassName.bind(this);
        this.getSelectedMonth = this.getSelectedMonth.bind(this);
        this.createChart = this.createChart.bind(this);
        this.getDebitAndCredit = this.getDebitAndCredit.bind(this);
        this.deleteChart = this.deleteChart.bind(this);
    }

    checkValueExist = (value, collection) => {
        let doesExist = false;
        for (let i = 0; collection.length > i; i++) {
            if (value === collection[i]) {
                doesExist = true;
            }
        }
        return doesExist;
    }

    deleteChart = () => {
        this.chartData.length = 0;
        let chart = document.getElementById("bar-visual");
        let chartData = chart.childNodes;
        chart.removeChild(chartData[0]);
    }

    createChart = (arr) => {
        let chart = window.anychart.column();
        chart.data(arr);
        chart.container("bar-visual");
        chart.draw();
    }

    getDebitAndCredit = (props, value) => {
        let credit = props.credit;
        let debit = props.debit;
        let parentArr = [credit, debit];
        for (let i = 0; parentArr.length > i; i++) {
            let thisArr = parentArr[i];
            let itemName = null;
            let arr = [];
            let count = 0;
            for (let j = 0; thisArr.collection.length > j; j++) {
                let item = thisArr.collection[j];
                itemName = (thisArr.isIncome) ? "Credits" : "Debits";

                if (item.TimeSpan === value) {
                    count = count + item.Amount;
                }
            }
            arr.push(itemName, count);
            this.chartData.push(arr);
        }
        return this.createChart(this.chartData);
    }

    getAmountOwed = (props, value) => {
        let arr = props.debit.collection;
        for (let i = 0; arr.length > i; i++) {
            let item = arr[i];
            let amount = item.AmountOwed;
            let dataArr = [];
            if (item.TimeSpan === value) {
                dataArr.push(item.Name);
                dataArr.push(amount);
                this.chartData.push(dataArr);
            }
        }
        return this.createChart(this.chartData);
    }

    getBillTypes = (props, value) => {
        let arr = props.debit.collection;
        for (let i = 0; arr.length > i; i++) {
            let item = arr[i];
            let amount = item.AmountOwed;
            let dataArr = [];
            if (item.TimeSpan === value) {
                dataArr.push(item.Type);
                dataArr.push(amount);
                this.chartData.push(dataArr);
            }
        }
        return this.createChart(this.chartData);
    }

    checkElementsHasClassName = (parentNode, stringValue) => {
        let childBtnList = parentNode.getElementsByTagName("button");
        for (let i = 0; childBtnList.length > i; i++) {
            if (childBtnList[i].className.includes(stringValue)) {
                childBtnList[i].classList.remove(stringValue);
                this.deleteChart();
            }
        }
    }

    getSelectedMonth = (arr) => {
        for (let i = 0; arr.length > i; i++) {
            let child = arr[i];
            let parent = child.closest(".parent");
            if (parent.getElementsByClassName("btn-container")) {
                return child.innerHTML;
            }
        }
        return null;
    }

    setChartParameters = (e) => {
        let thisBtn = e.target;
        let getAttr = thisBtn.getAttribute("methodname");
        let parent = thisBtn.closest(".parent");
        let selected = document.getElementsByClassName("selected");

        if (getAttr != null) { this.methodname = getAttr; }
        if (!parent.classList.contains("parent")) { return; }        
        if (parent.hasChildNodes()) { this.checkElementsHasClassName(parent, "selected"); }
        thisBtn.className += "selected";
        let thisMonth = this.getSelectedMonth(selected);

        if (selected.length > 1 && thisMonth != null) {
            eval("this." + this.methodname)(this.props, thisMonth);
        }
    }

    getAllMonths = (collection, arr) => {
        for (let i = 0; collection.length > i; i++) {
            let timeSpan = collection[i].TimeSpan;
            if (arr.length > 0) {
                let doesExist = this.checkValueExist(timeSpan, arr);
                if (!doesExist) { arr.push(timeSpan) }
            } else {
                arr.push(timeSpan);
            }
        }
        return arr;
    }

    componentWillReceiveProps(props) { // this may be depreciated
        this.setState({ data: props.data });
        let arr = [];
        arr = this.getAllMonths(this.props.credit.collection, arr);
        arr = this.getAllMonths(this.props.debit.collection, arr);
        this.months = arr;
    }

    render() {
        const months = this.months;
        return (            
            <section className="ResultBody col-12 remove-padding">
                <div className="row">
                    <div className="col-centered col-8 months-list">
                        <div className="col-12">
                            <div className="row col-centered parent">
                                {months.map((item) => <div className="col-3 btn-container"><button onClick={this.setChartParameters} key={item}>{item}</button></div>)}
                            </div>
                        </div>
                    </div>
                </div>
                <div className="data-section col-12 remove-padding">
                    <div className="row">
                        <div className="col-1 tab-btn remove-padding">
                            <div className="col-12 parent">
                                <div className="row tab-btn-container">
                                    <button methodname="getDebitAndCredit" onClick={this.setChartParameters}>Debit <br /> vs Credit</button>
                                </div>
                                <div className="row tab-btn-container">
                                    <button methodname="getAmountOwed" onClick={this.setChartParameters}>Amount <br/> Owed</button>
                                </div>
                                <div className="row tab-btn-container">
                                    <button methodname="getBillTypes" onClick={this.setChartParameters}>Bill Types</button>
                                </div>
                            </div>
                        </div>
                        <div id="bar-visual" className="col-11"></div>
                    </div>
                </div>
            </section>
        );
    }
}

export default ResultBody