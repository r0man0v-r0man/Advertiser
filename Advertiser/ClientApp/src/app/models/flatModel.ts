import { UploadFile } from 'ng-zorro-antd';

export interface FlatModel{
    rooms: number;
    isActive: boolean;
    price: number;
    description: string;
    file: UploadFile
    id: number;
}