import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public aggregatedata: AggregateData;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<CarSalesMatrix>(baseUrl + 'api/sales').subscribe(result => {
      this.aggregatedata = new AggregateData(result);
    }, error => console.error(error));
  }
}

interface CarSalesMatrix {
  columns: string[];
  rows: string[][];
}

class AggregateData {
  columns: string[];
  rows: string[][];

  columnData: number[][];

  get averages() { return this.columnData.map(c => getAverage(c)); }
  get medians() { return this.columnData.map(c => getMedian(c)); }
  get totals() { return this.columnData.map(c => getTotal(c)); }

  constructor(tabledata: CarSalesMatrix) {
    this.rows = tabledata.rows;
    this.columns = tabledata.columns;

    this.columnData = this.rows.map(r => r.slice(1).map(n => [n]))
      .reduce((baseRow, nextRow) => baseRow.map((x, i) => x.concat(nextRow[i])))
      .map(values => values.map(s => Number(s)));
  }
}

function getAverage(list: number[]) {
  return list.reduce((base, next) => base + next) / list.length;
}

function getTotal(list: number[]) {
  return list.reduce((first, next) => first + next);
}

function getMedian(list: number[]) {
  if (list.length === 0) return 0;

  list.sort((a, b) => a - b);

  var middle = Math.floor(list.length / 2);

  return list.length % 2 ? list[middle] : (list[middle - 1] + list[middle]) / 2.0;
}
