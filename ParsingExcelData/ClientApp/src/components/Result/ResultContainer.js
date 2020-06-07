import React, { Component } from 'react';
import ResultHeader from './ResultChildren/ResultHeader';
import ResultBody from './ResultChildren/ResultBody';

export class ResultContainer extends Component {
    constructor(props) {
        super(props);
        this.calculateData = this.calculateData.bind(this);
        this.storeObjects = this.storeObjects.bind(this);
        this.setData = this.setData.bind(this);
        this.credit = { isIncome: true, collection: [], count: 0 };
        this.debit = { isIncome: false, collection: [], count: 0 };
    }

    storeObjects = (obj, props) => {
        let creditArr = [];
        let debitArr = [];

        let propsData = props.data;
        for (let i = 0; propsData.length > i; i++) {
            let amount = parseFloat(propsData[i].Amount); // backend needs to take care of this
            propsData[i].Amount = amount;
            if (propsData[i].Type === "Income") {
                creditArr.push(propsData[i]);
            } else {
                debitArr.push(propsData[i]);
            }
        }
        return (obj.isIncome) ? creditArr : debitArr;
    }

    calculateData = (obj, props) => {
        let creditCount = 0;
        let debitCount = 0;
        let propsData = props.data;
        for (let i = 0; propsData.length > i; i++) {
            let amount = parseFloat(propsData[i].Amount); // backend needs to take care of this
            propsData[i].Amount = amount;
            if (isNaN(amount)) { amount = 0; }
            if (propsData[i].Type === "Income") {
                creditCount = creditCount + amount;
            } else {
                debitCount = debitCount + amount;
            }
        }
        return (obj.isIncome) ? creditCount.toFixed(2) : debitCount.toFixed(2);
    }

    setData = (finance, props) => {
        finance.collection = this.storeObjects(finance, props);
        finance.count = this.calculateData(finance, props);
        return finance;
    }

    componentWillReceiveProps(props) { // this may be depreciated
        let results = document.getElementsByClassName("ResultContainer");
        results[0].classList.remove("hidden");
        this.setState({ data: props.data });
        this.credit = this.setData(this.credit, props);
        this.debit = this.setData(this.debit, props);

    }

    render() {
        return (
            <section data={this.props.data} className="container ResultContainer hidden">
                <ResultHeader credit={this.credit} debit={this.debit} />   
                <ResultBody credit={this.credit} debit={this.debit} />
            </section>
        );
    }
}

export default ResultContainer;