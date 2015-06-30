#ifndef __SEMAPHORE_STRCUT_H__
#define __SEMAPHORE_STRUCT_H__

/*
 * the following are operation structures for semaphore control
 * the acquire will decrement the semaphore by 1
 * and the release will increment the semaphore by 1.
 * Both of these structs are initialized to work with the 1st
 * semaphore we allocate.
 */

struct sembuf acquire_operation = { 0, -1, SEM_UNDO };
struct sembuf release_operation = { 0, 1, SEM_UNDO };

/*
 * the initial value of the semaphore will be 1, indicating that
 * our critical region is ready is ready for use by the first
 * task that can successfully decrement our shared semaphore
 */

unsigned short init_values[1] = { 1 };

#endif
