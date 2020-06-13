import React, { Component } from 'react';

export class ResultHeader extends Component {
    constructor(props) {
        super(props);
        this.largestCredit = { name: null, value: null};
        this.largestDebit = { name: null, value: null };;
        this.findLargestValue = this.findLargestValue.bind(this);
        this.incrementValues = this.incrementValues.bind(this);;
    }

    incrementValues = (value, name, collection) => {
        for (let i = 0; collection.length > i; i++) {
            if (name == collection[i].Name) {
                value = value + collection[i].Amount;
            }
        }
        return value.toFixed(2);
    }

    findLargestValue = (prop) => {
        let lastValue = 0;
        let finalValue = 0;
        let obj = { name: null, value: null };

        for (let i = 0; prop.length > i; i++) {
            let value = prop[i].Amount;
            if (lastValue == 0 || value > lastValue) {
                lastValue = value;
                obj.name = prop[i].Name;;
                obj.value = value;
            }        
        }
        finalValue = this.incrementValues(finalValue, obj.name, prop);

        return {
            name: obj.name,
            value: finalValue
        };
    }

    componentWillReceiveProps(props) { // this may be depreciated
        this.setState({ data: props.data });
        this.largestCredit = this.findLargestValue(props.credit.collection);
        this.largestDebit = this.findLargestValue(props.debit.collection);
    }

    render() {
        return (
            <div className="ResultHeader col-12">
                <div className="row">
                    <div className="col-6 left">
                        <div className="row">
                            <div className="col top">
                                <p className="align-left">Amount spent this year</p>
                                <h2 className="align-left">{this.props.debit.count}</h2>
                            </div>
                        </div>
                        <div className="row">
                            <div className="col bottom">
                                <p className="align-left">Amount made this year</p>
                                <h4 className="align-left">{this.props.credit.count}</h4>
                            </div>                    
                        </div>
                    </div>
                    <div className="col-6 right">                    
                        <p><i>{this.largestDebit.name}</i> is the biggest issue with <span className="debit"><i>{this.largestDebit.value}</i></span> in spending</p>
                        <p><i>{this.largestCredit.name}</i> is the reason you made <span className="credit"><i>{this.largestCredit.value}</i></span></p>
                    </div>
                </div>
            </div>
        );
    }
}

export default ResultHeader;