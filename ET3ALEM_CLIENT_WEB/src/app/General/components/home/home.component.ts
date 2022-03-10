import {
  Component, ElementRef,
  HostListener,
  OnInit,
  QueryList, Renderer2,
  ViewChildren
} from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private ElByClassName: ElementRef, private renderer: Renderer2) {
  }

  ngOnInit(): void {
  }

  @HostListener('window:scroll', ['$event']) onWindowScroll(_) {
    const scrollPosition = window.scrollY;
    this.setMoveXOnScrollBehavior(scrollPosition);
    this.setMoveUpOnScrollBehavior(scrollPosition);
  }

  private setMoveXOnScrollBehavior(scrollPosition: number) {
    const moveXonScroll = (<HTMLElement>this.ElByClassName.nativeElement).querySelector(
      '.moveXonScroll'
    );
    this.renderer.setStyle(moveXonScroll, 'transform', `translateX(${scrollPosition * -0.5}px)`);
  }

  private setMoveUpOnScrollBehavior(scrollPosition: number) {
    let moveUpOnScrollElements = (<HTMLElement>this.ElByClassName.nativeElement).querySelectorAll(
      '.moveUpOnScroll'
    );

    moveUpOnScrollElements.forEach(
      (moveUpOnScroll, index) => {
        this.renderer.setStyle(moveUpOnScroll, 'transform', `translateY(${scrollPosition * (index / 5) * -1}px)`);
        this.renderer.setStyle(moveUpOnScroll, 'opacity', `${100 / (scrollPosition + 50)}`);
      }
    );
  }
}
