# import numpy as np
from numpy.random import randint
# from random import random as rnd
# from random import gauss, randrange
import random
import pandas as pd

# Делаем 4 хромосомы

def create_chromosome(df):
    index_list = list()
    # chromosome1 = list()
    index = []
    for x in range(0, 4):
        number = random.randint(0, 3)
        while number in index:
            number = random.randint(0, 3)
        # chromosome1.append((list(df[x]))[number])
        index.append(number)
    index_list.append(index)

    index = []
    for x in range(0, 4):
        number = random.randint(0, 3)
        while number in index:
            number = random.randint(0, 3)
        index.append(number)
    index_list.append(index)

    index = []
    for x in range(0, 4):
        number = random.randint(0, 3)
        while number in index:
            number = random.randint(0, 3)
        index.append(number)
    index_list.append(index)

    index = []
    for x in range(0, 4):
        number = random.randint(0, 3)
        while number in index:
            number = random.randint(0, 3)
        index.append(number)
    index_list.append(index)
    print(index_list)

    return index_list

# Найдем лучшую и худшую из них
def best_and_worst_chromosome(index_list, df):
    chromosome = list()
    for x in range(4):
        chromosome.append([df[y][index_list[x][y]] for y in range(0, len(index_list[x]))])
    #print(chromosome)
    max_sum = max(sum(chromosome[0]), sum(chromosome[1]), sum(chromosome[2]), sum(chromosome[3]))
    min_sum = min(sum(chromosome[0]), sum(chromosome[1]), sum(chromosome[2]), sum(chromosome[3]))
    print('max =',max_sum)
    print('min =',min_sum)
    return max_sum, min_sum, chromosome

# Отбираем три лучшие из них для скрещивания

def delete_worst_chromosome(index_list2,old_population, max_sum):
    for x in range(0, len(old_population)):
        if sum(old_population[x]) == max_sum:
            old_population.remove(old_population[x])
            index_list2.remove(index_list2[x])
            break;
    return old_population,index_list2

# Скрещиваем
## Находим самую сильную особь

def find_best_chromosome(old_population, index_list2, min_sum):
    for x in range(0, len(old_population)):
        if sum(old_population[x]) == min_sum:
            strong_individual = index_list2[x]
            old_population.remove(old_population[x])
            index_list2.remove(index_list2[x])
            break

    return strong_individual, old_population, index_list2

## Создаем новое поколение
def create_new_population(strong_individual, index_list_without_strong, index_list):
    new_chromosome1 = strong_individual
    new_chromosome2 = strong_individual[0:2] + [x for x in reversed(strong_individual[2:4])]
    new_chromosome3 = [x for x in reversed(strong_individual[0:2])] + strong_individual[2:4]
    new_chromosome4 = index_list_without_strong[0][0:2] + [x for x in reversed(index_list_without_strong[0][2:4])]
    new_chromosome5 = [x for x in reversed(index_list_without_strong[0][0:2])] + (index_list_without_strong[0][2:4])
    new_chromosome6 = index_list_without_strong[1][0:2] + [x for x in reversed(index_list_without_strong[1][2:4])]
    new_chromosome7 = [x for x in reversed(index_list_without_strong[1][0:2])] + index_list_without_strong[1][2:4]

    new_index_list = list()

    for index_chro in [new_chromosome1, new_chromosome2, new_chromosome3, new_chromosome4, new_chromosome5,
                       new_chromosome6, new_chromosome7]:
        new_index_list.append(index_chro)

    chromosomes = []

    for x in range(7):
        chromosomes.append([df[y][new_index_list[x][y]] for y in range(0, len(new_index_list[x]))])

    #     for chro in [new_chromosome1, new_chromosome2, new_chromosome3, new_chromosome4, new_chromosome5, new_chromosome6, new_chromosome7]:
    #         chromosomes.append(chro)

    def remove_worst(chromosomes, new_index_list):
        chromosomes_sum = [sum(chromosomes[x]) for x in range(0, len(chromosomes))]
        # print(chromosomes_sum)
        worst = max(chromosomes_sum)
        for x in range(0, len(chromosomes)):
            if sum(chromosomes[x]) == worst:
                weak_individual = x
                chromosomes.remove(chromosomes[x])
                new_index_list.remove(new_index_list[x])
                # print(new_index_list[x])
                break

    for i in range(3):
        remove_worst(chromosomes, new_index_list)
    print(new_index_list)

    print('new_chromosome1', chromosomes[0])
    print('new_chromosome2', chromosomes[1])
    print('new_chromosome3', chromosomes[2])
    print('new_chromosome4', chromosomes[3])

    return chromosomes, chromosomes[0], chromosomes[1], chromosomes[2], chromosomes[3], new_index_list

## Мутации, находим самую слабую особь и заменяем ей один ген
def mutation(index_list, first_grapf):
    chromosomes = []
    for x in range(4):
        chromosomes.append([df[y][index_list[x][y]] for y in range(0, len(index_list[x]))])
    print('Мутация')
    max_sum_new = max(sum(chromosomes[0]), sum(chromosomes[1]), sum(chromosomes[2]), sum(chromosomes[3]))
    # Мутируем один случайный ген у самой слабой особи
    for x in range(0,len(chromosomes)):
        if sum(chromosomes[x]) == max_sum_new:
            weak_individual = x
            break
    rndm_gen = randint(0, 4)
    index_list[x][rndm_gen], index_list[x][3 - rndm_gen] = index_list[x][3 - rndm_gen], index_list[x][rndm_gen]
    return index_list

# Собранный полностью генетический алгоритм
a = [[0,2,2,3],
     [2,1,4,3],
     [3,4,1,5],
     [3,4,3,0]]
df = pd.DataFrame(a)
first_grapf = pd.DataFrame(a)

index_list = create_chromosome(df)
for i in range(20):
    print('Итерация:', i)
    max_sum, min_sum, chromosome = best_and_worst_chromosome(index_list, df)
    index_list2 = index_list.copy()
    old_population,index_list2 = delete_worst_chromosome(index_list2,chromosome, max_sum)
    strong_individual, old_population, index_list_without_strong = find_best_chromosome(old_population,index_list2, min_sum)
    new_population,new_chromosome1,new_chromosome2,new_chromosome3,new_chromosome4,index_list = create_new_population(
        strong_individual,index_list_without_strong, index_list
    )
    index_list = mutation(index_list, first_grapf)
print('Минимальное расстояние:', min_sum)
