import { Directive, ViewContainerRef } from '@angular/core';

@Directive({
  selector: '[dynamicComponentHost]'
})
export class DynamicComponentHostDirective {

  constructor(public viewContainerRef: ViewContainerRef) { }

}
