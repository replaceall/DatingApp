import { Injectable } from '@angular/core';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';
import { CanDeactivate } from '@angular/router';

@Injectable()
export class PreventUnsavedChangesGuard implements CanDeactivate<MemberEditComponent> {
    canDeactivate(component: MemberEditComponent ) {
        if(component.editForm.dirty){
            return confirm('Are you sure you want to continue? you have made some chnages.');
        }
        return true;
    }
}