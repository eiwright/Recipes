import {Recipe} from './Recipe'

export interface Ingredient {
    id: string | undefined
    name: string
    quantity: string
    calories?: number

    recipies: Recipe[]
  }
  