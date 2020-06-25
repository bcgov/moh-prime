import { MatTableDataSource } from '@angular/material/table';

export class MatTableDataSourceUtils {
  /**
   * @description
   * Update an item in a datasource based on a predicate.
   *
   * @example
   * this.dataSource.data = MatTableDataSourceUtils
   *   .update(this.dataSource, 'id', updateModel);
   */
  public static update<T>(dataSource: MatTableDataSource<T>, key: string, updateModel: T) {
    return dataSource.data.map((currentModel: T) =>
      (currentModel[key] === updateModel[key]) ? updateModel : currentModel
    );
  }

  /**
   * @description
   * Remove item(s) from a datasource based on a predicate.
   *
   * @example
   * this.dataSource.data = MatTableDataSourceUtils
   *   .delete(this.dataSource, 'id', model.id);
   */
  public static delete<T>(dataSource: MatTableDataSource<T>, key: string, value: any) {
    return dataSource.data.filter((model: T) =>
      model[key] !== value
    );
  }
}
