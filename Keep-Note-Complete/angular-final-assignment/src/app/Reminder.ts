        export class Reminder {
            id: Number;
            name: string;
            description:string;    
            createdBy:string;
            type:string;
            createdDate:Date;
            
            constructor() {
              this.name = '';
              this.description = '';
              this.type ='';
            }
         }
          