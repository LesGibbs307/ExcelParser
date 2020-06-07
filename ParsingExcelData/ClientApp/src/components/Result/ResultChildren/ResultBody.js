import React, { Component } from 'react';

export class ResultBody extends Component {
    constructor(props) {
        super(props);
        this.months = [];
        this.methodname = null;
        this.getAllMonths = this.getAllMonths.bind(this);
        this.checkValueExist = this.checkValueExist.bind(this);
        this.setChartParameters = this.setChartParameters.bind(this);
        this.getAmountOwed = this.getAmountOwed.bind(this);
        this.getBillTypes = this.getBillTypes.bind(this);
        this.checkElementsHasClassName = this.checkElementsHasClassName.bind(this);
        this.getSelectedMonth = this.getSelectedMonth.bind(this);
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

    getAmountOwed = (props) => {
        debugger;
        console.log(props);
    }

    getBillTypes = (props) => {
        debugger;
        console.log(props);
    }

    checkElementsHasClassName = (parentNode, stringValue) => {
        for (let i = 0; parentNode.childNodes.length > i; i++) {b
            if (parentNode.childNodes[i].className == stringValue) {
                parentNode.childNodes[i].classList.remove(stringValue);
                return;
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
        debugger;
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
            eval(this.methodname)(this.props, thisMonth);
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
                    <div className="col-1 tab-btn remove-padding">
                        <div className="col-12 parent">
                            <div className="row btn-container">
                                <button methodname="" onClick={this.setChartParameters}>Debit <br /> vs Credit</button>
                            </div>
                            <div className="row tab-btn-container">
                                <button methodname="getAmountOwed" onClick={this.setChartParameters}>Amount <br/> Owed</button>
                            </div>
                            <div className="row tab-btn-container">
                                <button methodname="getBillTypes" onClick={this.setChartParameters}>Bill Types</button>
                            </div>
                        </div>
                    </div>
                    <div className="col-11 bar-visual">
                    </div>
                </div>
            </section>
        );
    }
}

export default ResultBody