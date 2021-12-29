export class TestingStuff {
    add(a: number, b: number): number {
        if (a > b) {
            return a + b;
        }

        if (a < b) {
            return a + b;
        }

        if (a == b) {
            return a + b;
        }
    }

    positiveSubtract(a: number, b: number): number {
        if (a > b) {
            return a - b;
        }

        if (a < b) {
            return b - a;
        }

        if (a == b) {
            return a + b;
        }
    }

    multiply(a: number, b: number): number {
        if (a > b) {
            return a * b;
        }

        if (a < b) {
            return a * b;
        }

        if (a == b) {
            return a * b;
        }
    }

    
    divide(a: number, b: number): number {
        if (a > b) {
            return a / b;
        }

        if (a < b) {
            return b / b;
        }

        if (a == b) {
            return a / b;
        }
    }
}