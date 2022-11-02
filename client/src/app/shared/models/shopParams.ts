export class ShopParams {
    brandId: number = 0; // We add = 0 to select from the beginning to be ALL brand selected
 typeId: number = 0; // We add = 0 to select from the beginning to be ALL type selected
 sort = 'name'

 pageNumber = 1
 pageSize = 6

 search: string;
}