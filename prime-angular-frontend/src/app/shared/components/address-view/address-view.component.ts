import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';

import { Address, AddressLine, optionalAddressLineItems } from '@lib/models/address.model';

@Component({
  selector: 'app-address-view',
  templateUrl: './address-view.component.html',
  styleUrls: ['./address-view.component.scss']
})
export class AddressViewComponent implements OnInit {
  /**
   * @description
   * Address section title.
   */
  @Input() public title: string;
  /**
   * @description
   * Address for viewing.
   */
  @Input() public address: Address;
  /**
   * @description
   * Show a message indicating an address does not exist.
   */
  @Input() public showIfEmpty: boolean;
  /**
   * @description
   * Show the redirect icon.
   */
  @Input() public showRedirect: boolean;
  /**
   * @description
   * Route path for redirection.
   */
  @Input() public redirectRoutePath: string | string[];
  /**
   * @description
   * Determines the definition of an empty address.
   */
  @Input() public optionalAddressLineItems: ('id' | AddressLine)[];
  /**
   * @description
   * Emit route event when no redirect route path is provided.
   */
  @Output() public route: EventEmitter<void>;

  constructor(
    private router: Router
  ) {
    this.optionalAddressLineItems = optionalAddressLineItems;
    this.route = new EventEmitter<void>();
  }

  public get hasAddress(): boolean {
    return Address.isNotEmpty(this.address, this.optionalAddressLineItems);
  }

  public onRoute(routePath: string | string[]) {
    (this.redirectRoutePath)
      ? this.router.navigate(this.getRoutePath(routePath))
      : this.route.emit();
  }

  public ngOnInit(): void {
  }

  private getRoutePath(routePath: string | string[]) {
    return (Array.isArray(routePath))
      ? routePath
      : [routePath];
  }
}
