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
  public static update<T>(dataSource: MatTableDataSource<T>, key: string, updateModel: T): T[] {
    return dataSource.data.map((currentModel: T) =>
      (currentModel[key] === updateModel[key]) ? updateModel : currentModel
    );
  }

  /**
   * @description
   * Remove item(s) from a datasource by ID.
   *
   * @example
   * this.dataSource.data = MatTableDataSourceUtils
   *   .delete(this.dataSource, model.id);
   */
  public static deleteById<T>(dataSource: MatTableDataSource<T>, value: any): T[] {
    return MatTableDataSourceUtils.delete(dataSource, 'id', value);
  }

  /**
   * @description
   * Remove item(s) from a datasource based on a predicate.
   *
   * @example
   * this.dataSource.data = MatTableDataSourceUtils
   *   .delete(this.dataSource, 'id', model.id);
   */
  public static delete<T>(dataSource: MatTableDataSource<T>, deletionKey: string, deletionValue: any): T[] {
    return dataSource.data.filter((model: T) =>
      model[deletionKey] !== deletionValue
    );
  }

  /**
   * @description
   * Remove nested item(s) from a datasource by ID.
   *
   * @example
   * this.dataSource.data = MatTableDataSourceUtils
   *   .deleteRelatedById(this.dataSource, parent.id, child.id);
   */
  public static deleteRelatedById<T, S>(
    dataSource: MatTableDataSource<T>,
    parentValue: any,
    relationKey: string,
    deletionValue: any): T[] {
    return MatTableDataSourceUtils.deleteRelated<T, S>(dataSource, 'id', parentValue, relationKey, 'id', deletionValue);
  }


  /**
   * @description
   * Remove nested item(s) from a datasource based on a predicate.
   *
   * @example
   * this.dataSource.data = MatTableDataSourceUtils
   *   .deleteRelated(this.dataSource, 'id', parent.id, 'id', child.id);
   */
  public static deleteRelated<T, S>(
    dataSource: MatTableDataSource<T>,
    parentKey: string,
    parentValue: any,
    relationKey: string,
    deletionKey: string,
    deletionValue: any): T[] {
    const data = dataSource.data;
    const index = data.findIndex((model: T) => model[parentKey] === parentValue);
    if (index > -1 && Array.isArray(data[index][relationKey])) {
      data[index][relationKey] = data[index][relationKey]
        .filter((model: S) => model[deletionKey] !== deletionValue);
    }

    return data;
  }
}
