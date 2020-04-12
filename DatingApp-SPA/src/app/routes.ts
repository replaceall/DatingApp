import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { MembersListComponent } from './members/members-list/members-list.component';
import { ListComponent } from './list/list.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';
import { MembersDetailComponent } from './members/members-detail/members-detail.component';
import { MemberDetailResolver } from './resolvers/member-detail.resolver';
import { MemberListResolver } from './resolvers/member-list.resolver';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberEditResolver } from './resolvers/member-edit.resolver';
import { PreventUnsavedChangesGuard } from './_guards/prevent-unsaved-chnages.guard';

export const appRoutes: Routes = [
    {path: '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            {path: 'members', component: MembersListComponent,
                              resolve: {users: MemberListResolver}},
            {path: 'members/:id', component: MembersDetailComponent,
                                  resolve: {user: MemberDetailResolver}},
            {path: 'member/edit', component: MemberEditComponent,
                                resolve: {user: MemberEditResolver}, canDeactivate: [PreventUnsavedChangesGuard]},
            {path: 'messages', component: MessagesComponent},
            {path: 'lists', component: ListComponent},
        ]
    },
    {path: '**', redirectTo: '', pathMatch: 'full'},
];
