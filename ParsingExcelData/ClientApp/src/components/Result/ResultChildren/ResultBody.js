import React, { Component } from 'react';

export class ResultBody extends Component {
    constructor(props) {
        super(props);
        this.months = [];
        this.getAllMonths = this.getAllMonths.bind(this);
        this.checkValueExist = this.checkValueExist.bind(this);

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
            <section className="ResultBody col-12">
                <div className="row">
                    <div className="col-centered col-8 months-list">
                        <div className="col-12">
                            <div class="row col-centered">
                                {months.map((item, i) => <div className="col-3"><button key={i}>{item}</button></div>)}
                            </div>
                        </div>
                    </div>
                </div>
                <div className="data-section col-12">
                    <div className="col-1 tab-btn">
                    </div>
                    <div className="col-11 bar-visual">
                    </div>
                </div>
            </section>
        );
    }

}

export default ResultBody