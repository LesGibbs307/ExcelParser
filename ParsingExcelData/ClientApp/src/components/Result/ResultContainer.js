import React, { Component } from 'react';
import ResultHeader from './ResultChildren/ResultHeader';

export class ResultContainer extends Component {
    constructor(props) {
        super(props);
        this.calculateData = this.calculateData.bind(this);
        this.storeObjects = this.storeObjects.bind(this);
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
        return (obj.isPositive) ? creditArr : debitArr;
    }

    calculateData = (obj, props) => {
        let creditCount = 0;
        let debitCount = 0;
        let propsData = props.data;
        for (let i = 0; propsData.length > i; i++) {
            let amount = parseFloat(propsData[i].Amount); // backend needs to take care of this
            propsData[i].Amount = amount;
            if (amount === NaN) {
                amount = 0;
            }
            if (propsData[i].Type === "Income") {
                creditCount = creditCount + amount;
            } else {
                debitCount = debitCount + amount;
            }
        }
        return (obj.isIncome) ? creditCount : debitCount;
    }

    componentWillReceiveProps(props) { // this may be depreciated
        this.setState({ data: props.data });
        let credit = { isIncome: true, collection:[], count:0 };
        let debit = { isIncome: false, collection:[], count:0 };
        credit.collection = this.storeObjects(credit, props);
        debit.collection = this.storeObjects(debit, props);
        credit.count = this.calculateData(credit, props);
        debit.count = this.calculateData(debit, props);
    }

    render() {
        return (
            <section data={this.props.data} className="ResultContainer col-xs-12">
                <ResultHeader />
            </section>
        );
    }
}

export default ResultContainer;
